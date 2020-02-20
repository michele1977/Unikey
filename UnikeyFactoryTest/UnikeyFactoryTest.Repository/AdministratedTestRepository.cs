using Ninject;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.UI;
using AutoMapper;
using UnikeyFactoryTest.Context;
using UnikeyFactoryTest.Domain;
using UnikeyFactoryTest.Domain.Enums;
using UnikeyFactoryTest.IRepository;
using UnikeyFactoryTest.Mapper;

namespace UnikeyFactoryTest.Repository
{
    public class AdministratedTestRepository : IAdministratedTestRepository, IDisposable
    {
        private readonly TestPlatformDBEntities _ctx;

        private readonly IKernel Kernel;
        public AdministratedTestRepository()
        {
            _ctx = new TestPlatformDBEntities();
        }

        public AdministratedTestRepository(TestPlatformDBEntities ctx,IKernel kernel)
        {
            Kernel = kernel;
            _ctx = ctx;
        }

        public AdministratedTestBusiness Add(AdministratedTestBusiness adTest)
        {
            try
            {
                var mapper = Kernel.Get<IMapper>("Heavy");
                var newAdTestDb = mapper.Map<AdministratedTestBusiness, AdministratedTest>(adTest);
                _ctx.AdministratedTests.Add(newAdTestDb);
                _ctx.SaveChanges();
                adTest = mapper.Map<AdministratedTest, AdministratedTestBusiness>(newAdTestDb);
                return adTest;
            }
            catch 
            {
                throw new Exception("Save Failed");
            }
        }

        public async Task<AdministratedTestBusiness> GetAdministratedTestById(int testId)
        {
            var task = await _ctx.AdministratedTests.FirstOrDefaultAsync(x => x.Id.Equals(testId));

            if (task == null)
            {
                throw new Exception("Not valid id");
            }
            var mapper = Kernel.Get<IMapper>("Heavy");
            var result = mapper.Map<AdministratedTest, AdministratedTestBusiness>(task);
            return result;
        }

        public async Task<List<AdministratedTestBusiness>> GetAdministratedTests()
        {
            var administratedTestListTask = await _ctx.AdministratedTests.Select(t => new AdministratedTestBusiness()
            {
                Id = t.Id,
                TestSubject = t.TestSubject,
                Date = t.Date,
                Score = t.Score,
                MaxScore = t.MaxScore
            }).ToListAsync();

            return administratedTestListTask;
        }

        public async Task<List<AdministratedTestBusiness>> GetAdministratedTestsByTestId(int testId)
        { 
            var adTestList = await _ctx.AdministratedTests.Where(t => t.TestId == testId).ToListAsync();
            var mapper = Kernel.Get<IMapper>("Heavy");
            var filteredList2 = mapper.Map<List<AdministratedTest>, List<AdministratedTestBusiness>>(adTestList);
            return filteredList2;
        }

        public async Task<int> GetState(int AdministratedTestId)
        {
            var myTask = await _ctx.AdministratedTests.FirstOrDefaultAsync(x => x.Id.Equals(AdministratedTestId));
            var State = myTask.State;
            return (int)State;
        }

        #region DeleteAdministratedTest
        public async Task DeleteAdministratedTest(int administratedTestId)
        {
            var administratedTest = await CheckTestById(administratedTestId);
            _ctx.AdministratedTests.Remove(administratedTest);
            _ctx.SaveChanges();
        }

        private async Task<AdministratedTest> CheckTestById(int administratedTestId)
        {
            var administratedTest = await _ctx.AdministratedTests
                .FirstOrDefaultAsync(t => t.Id == administratedTestId);

            if (administratedTest == null)
            {
                throw new NullReferenceException("AdministratedTest not found at specified id");
            }

            return administratedTest;
        }
        #endregion

        #region Update_Save
        public async Task Update_Save(AdministratedTestBusiness test)
        {

            if (test == null)
            {
                throw new Exception("No test to update");
            }

            decimal score = 0;
            test.Date = DateTime.Today;
            test.Score = GetScore(test, score);

            try
            {
                var mapper = Kernel.Get<IMapper>("Heavy");
                var newValue = (EntityExtension) mapper.Map<AdministratedTestBusiness, AdministratedTest>(test);
                var oldValue = await _ctx.AdministratedTests.FirstOrDefaultAsync(x => x.Id == newValue.MyId);
                NewUpdate(newValue, oldValue);
            }

            catch (Exception)
            {
                throw new Exception("Update failed");
            }
        }

        public void NewUpdate(EntityExtension newValue, EntityExtension oldValue)
        {
            oldValue.SetFlatProperty(newValue);
            var toRemove = oldValue.Childs.Where(x => newValue.Childs.All(y => y.MyId != x.MyId)).ToList();
            var toAdd = newValue.Childs.Where(x => oldValue.Childs.All(y => y.MyId != x.MyId)).ToList();
            var toUpdate = newValue.Childs.Where(x => oldValue.Childs.Any(y => y.MyId == x.MyId)).ToList();

            foreach (var child in toRemove) oldValue.RemoveChild(child, _ctx);

            foreach (var child in toAdd) oldValue.AddChild(child, _ctx);

            foreach (var child in toUpdate)
            {
                var childToUpdate = oldValue.Childs.FirstOrDefault(x => x.MyId == child.MyId);
                NewUpdate(child, childToUpdate);
            }

            _ctx.SaveChanges();
        }

        private static decimal GetScore(AdministratedTestBusiness newTest, decimal score)
        {
            foreach (var q in newTest.AdministratedQuestions)
            {
                if ((q.AdministratedAnswers.FirstOrDefault(x => x.isSelected == true)) != null)
                    score = score + (q.AdministratedAnswers.FirstOrDefault(x => x.isSelected == true)?.Score ?? 0);
            }

            return score;
        }
        public async Task Update_Save_Question(AdministratedQuestionBusiness adQuestion)
        {
            var mapper = Kernel.Get<IMapper>("Heavy");
            var newQuestion = (EntityExtension)mapper.Map<AdministratedQuestionBusiness, AdministratedQuestion>(adQuestion);
            var oldQuestion = await _ctx.AdministratedQuestions.FirstOrDefaultAsync(x=>x.Id == adQuestion.Id);
            NewUpdate(newQuestion, oldQuestion);
        }

        //private async Task Update_Save_Answers(AdministratedQuestion q)
        //{
        //    foreach (var a in q.AdministratedAnswers)
        //    {
        //        var myTask2 = Task.Run(() =>
        //        {
        //            _ctx.AdministratedAnswers.FirstOrDefault(x => x.Id == a.Id).isSelected = false;
        //            _ctx.SaveChanges();

        //            if (a.isSelected == true)
        //            {
        //                _ctx.AdministratedAnswers.FirstOrDefault(x => x.Id == a.Id).isSelected = true;
        //                _ctx.SaveChanges();
        //            }
        //        });
        //        await myTask2;
        //    }
        //}
        #endregion

        public void Dispose()
        {
            _ctx.Dispose();
        }


    }
}