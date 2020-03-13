using AutoMapper;
using Ninject;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using UnikeyFactoryTest.Context;
using UnikeyFactoryTest.Domain;
using UnikeyFactoryTest.IRepository;

namespace UnikeyFactoryTest.Repository
{
    public class AdministratedTestRepository : IAdministratedTestRepository
    {
        private readonly TestPlatformDBEntities _ctx;

        private readonly IKernel Kernel;
        private IMapper Mapper;
        public bool IsContextNull { get; private set; }

        public AdministratedTestRepository(TestPlatformDBEntities ctx,IKernel kernel)
        {
            Kernel = kernel;
            _ctx = ctx;
            IsContextNull = false;
        }

        #region AddAdministatedTest
        public AdministratedTestBusiness Add(AdministratedTestBusiness adTest)
        {
            try
            {
                Mapper = Kernel.Get<IMapper>("Heavy");
                var newAdTestDb = Mapper.Map<AdministratedTestBusiness, AdministratedTest>(adTest);
                _ctx.AdministratedTests.Add(newAdTestDb);
                _ctx.SaveChanges();
                adTest = Mapper.Map<AdministratedTest, AdministratedTestBusiness>(newAdTestDb);
                return adTest;
            }
            catch 
            {
                throw new Exception("Save Failed");
            }
        }
        #endregion
        
        #region GetAdministratedTest
        public async Task<AdministratedTestBusiness> GetAdministratedTestById(int testId)
        {
            var administratedTest = await _ctx.AdministratedTests.FirstOrDefaultAsync(x => x.Id.Equals(testId));

            if (administratedTest == null)
            {
                throw new Exception("Not valid id");
            }
            Mapper = Kernel.Get<IMapper>("Heavy");
            var result = Mapper.Map<AdministratedTest, AdministratedTestBusiness>(administratedTest);
            return result;
        }

        public async Task<List<AdministratedTestBusiness>> GetAdministratedTests()
        {
            var administratedTestList = await _ctx.AdministratedTests.Select(t => new AdministratedTestBusiness()
            {
                Id = t.Id,
                TestSubject = t.TestSubject,
                Date = t.Date,
                Score = t.Score,
                MaxScore = t.MaxScore
            }).ToListAsync();

            return administratedTestList;
        }

        public async Task<List<AdministratedTestBusiness>> GetAdministratedTestsByTestId(int testId)
        { 
            var adTestList = await _ctx.AdministratedTests.Where(t => t.TestId == testId).ToListAsync();
            Mapper = Kernel.Get<IMapper>("Light");
            var filteredList = Mapper.Map<List<AdministratedTest>, List<AdministratedTestBusiness>>(adTestList);
            return filteredList;
        }

        public async Task<int> GetState(int administratedTestId)
        {
            var administratedTest = await _ctx.AdministratedTests.FirstOrDefaultAsync(x => x.Id.Equals(administratedTestId));
            var state = administratedTest.State;
            return (int)state;
        }

        public async Task<List<AdministratedTestBusiness>> GetAllFiltered(int pageNum, int pageSize, string filter)
        {
            return await _ctx.AdministratedTests
                .OrderBy(x => x.Id)
                .Where(test => test.TestSubject.Contains(filter))
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize).Select(t => 
                new AdministratedTestBusiness()
                {
                    Id = t.Id,
                    Title = t.Title,
                    Date = t.Date,
                    TestSubject = t.TestSubject
                })
                .ToListAsync();
        }

        public async Task<int> CountExTests()
        {
            return await _ctx.AdministratedTests.CountAsync();
        }

        public async Task<int> CountExTests(string filter)
        {
            return await _ctx.AdministratedTests.Where(t => t.TestSubject.Contains(filter)).CountAsync();
        }

        public async Task<Dictionary<string, int>> GetScoreAndMax(int id)
        {
            var test = _ctx.AdministratedTests.Find(id);
            var result = new Dictionary<string, int>()
            {
                {"score", test.Score},
                {"maxScore", test.MaxScore}
            };

            return result;
        }

        #endregion

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

            test.Score = GetScore(test);

            try
            {
                Mapper = Kernel.Get<IMapper>("Heavy");
                var newValue = (EntityExtension) Mapper.Map<AdministratedTestBusiness, AdministratedTest>(test);
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

        private static int GetScore(AdministratedTestBusiness newTest)
        {
            int score = 0;
            foreach (var q in newTest.AdministratedQuestions)
            {
                if ((q.AdministratedAnswers.FirstOrDefault(x => x.isSelected == true)) != null)
                    score = score + (q.AdministratedAnswers.FirstOrDefault(x => x.isSelected == true)?.Score ?? 0);
            }

            return score;
        }

        public async Task Update_Save_Question(AdministratedQuestionBusiness adQuestion)
        {
            Mapper = Kernel.Get<IMapper>("Heavy");
            var newQuestion = (EntityExtension)Mapper.Map<AdministratedQuestionBusiness, AdministratedQuestion>(adQuestion);
            var oldQuestion = await _ctx.AdministratedQuestions.FirstOrDefaultAsync(x=>x.Id == adQuestion.Id);
            NewUpdate(newQuestion, oldQuestion);
        }
        #endregion

        public void Dispose()
        {
            _ctx.Dispose();
            IsContextNull = true;
        }
    }
}