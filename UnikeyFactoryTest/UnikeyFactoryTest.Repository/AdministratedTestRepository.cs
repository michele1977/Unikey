using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using UnikeyFactoryTest.Context;
using UnikeyFactoryTest.Domain;
using UnikeyFactoryTest.IRepository;
using UnikeyFactoryTest.Mapper;

namespace UnikeyFactoryTest.Repository
{
    public class AdministratedTestRepository : IAdministratedTestRepository, IDisposable
    {
        private readonly TestPlatformDBEntities _ctx;

        public AdministratedTestRepository()
        {
            _ctx = new TestPlatformDBEntities();
        }

        public AdministratedTestRepository(TestPlatformDBEntities ctx)
        {
            _ctx = ctx;
        }

        public async Task<AdministratedTestBusiness> Add(AdministratedTestBusiness adTest)
        {
            
            
            var addTask = Task.Run(() =>
            {
                try
                {
                    var newAdTestDb = AdministratedTestMapper.MapDomainToDao(adTest);
                    _ctx.AdministratedTests.Add(newAdTestDb);
                    _ctx.SaveChanges();
                    adTest = AdministratedTestMapper.MapDaoToDomainHeavy(newAdTestDb);
                    return adTest;
                }
                catch (Exception ex)
                {
                    throw new Exception("Save Failed");
                }
            });
            return await addTask;
        }

        public async Task<AdministratedTestBusiness> GetAdministratedTestById(int testId)
        {
            var task = await Task.Run(() =>
            {
                return _ctx.AdministratedTests.FirstOrDefault(x => x.Id.Equals(testId));
            });

            if (task == null)
            {
                throw new Exception("Not valid id");
            }

            return AdministratedTestMapper.MapDaoToDomainHeavy(task);
        }

        public async Task<List<AdministratedTestBusiness>> GetAdministratedTests()
        {
            var administratedTestListTask = await Task.Run(() => _ctx.AdministratedTests.Select(t => new AdministratedTestBusiness()
            {
                Id = t.Id,
                TestSubject = t.TestSubject,
                Date = t.Date,
                Score = t.AdministratedQuestions
                    .SelectMany(q => q.AdministratedAnswers)
                    .Where(a => (bool) a.isSelected)
                    .Sum(a => a.Score)
            }).ToList());

            return administratedTestListTask;
        }

        public async Task<List<AdministratedTestBusiness>> GetAdministratedTestsByTestId(int testId)
        {
            var myTask = Task.Run(() =>
            {
                var filteredList = _ctx.AdministratedTests.Where(t => t.TestId == testId).ToListAsync();
                return filteredList;
            });
            var adTestList = await myTask;
            var filteredList2 = adTestList.Select(AdministratedTestMapper.MapDaoToDomainLight).ToList();
            return filteredList2;
        }

        public async Task<int> GetState(int AdministratedTestId)
        {
            var myTask = Task.Run(() =>
            {
                var FilteredState = _ctx.AdministratedTests.FirstOrDefault(x => x.Id.Equals(AdministratedTestId))
                    .State;
                return FilteredState;
            });
            var State = await myTask;
            return (int) State;
        }
        #region DeleteAdministratedTest
        public async Task DeleteAdministratedTest(int administratedTestId)
        {
            var administratedTest = CheckTestById(administratedTestId);

            var myTask = Task.Run(() =>
            {
                _ctx.AdministratedTests.Remove(administratedTest);
                _ctx.SaveChanges();
            });

            await myTask;
        }

        private AdministratedTest CheckTestById(int administratedTestId)
        {
            var administratedTest = _ctx.AdministratedTests
                .FirstOrDefault(t => t.Id == administratedTestId);

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
                throw new NullReferenceException("No test to update");
            }

            decimal score = 0;
            test.Date = DateTime.Today;
            test.Score = GetScore(test, score);
            var newValue = (EntityExtension)AdministratedTestMapper.MapDomainToDao(test);
            var oldValue = await Task.Run(() => (EntityExtension)(_ctx.AdministratedTests.FirstOrDefault(x => x.Id == newValue.MyId)));
            NewUpdate(newValue, oldValue);
        }

        public void NewUpdate(EntityExtension newValue, EntityExtension oldValue)
        {
            oldValue.SetFlatProperty(newValue);
            var toRemove = oldValue.Childs.Where(x => newValue.Childs.All(y => y.MyId != x.MyId)).ToList();
            var toAdd = newValue.Childs.Where(x => oldValue.Childs.All(y => y.MyId != x.MyId)).ToList();
            var toUpdate = newValue.Childs.Where(x => oldValue.Childs.All(y => y.MyId == x.MyId)).ToList();

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

        #endregion

        public void Dispose()
        {
            _ctx.Dispose();
        }

    }
}
