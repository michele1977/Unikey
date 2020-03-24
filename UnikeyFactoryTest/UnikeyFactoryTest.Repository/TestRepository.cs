using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Ninject;
using UnikeyFactoryTest.Context;
using UnikeyFactoryTest.Domain;
using UnikeyFactoryTest.Domain.Enums;
using UnikeyFactoryTest.IRepository;
using UnikeyFactoryTest.Mapper;

namespace UnikeyFactoryTest.Repository
{
    public class TestRepository : ITestRepository
    {
        private readonly TestPlatformDBEntities _ctx;
        private readonly IKernel _kernel;
        public bool IsContextNull => _ctx == null;

        public TestRepository(TestPlatformDBEntities myCtx, IKernel kernel)
        {
            _kernel = kernel;
            _ctx = myCtx;
        }

        public void SaveTest(Test test)
        {
            _ctx.Tests.Add(test);
            _ctx.SaveChanges();
        }

        public async Task<TestBusiness> GetTestByURL(string URL)
        {
            var result = await _ctx.Tests.FirstOrDefaultAsync(x => x.URL.Equals(URL));

            if (result == null)
            {
                throw new Exception($"Test not found");
            }

            var mapper = _kernel.Get<IMapper>("Heavy");
            return mapper.Map<Test, TestBusiness>(result);
        }

        public async Task<TestBusiness> GetTest(int testId)
        {
            var myTask = await _ctx.Tests.FirstOrDefaultAsync(t => t.Id == testId);

            if (myTask == null)
            {
                throw new Exception($"Test not found");
            }
            var mapper = _kernel.Get<IMapper>("Heavy");
            return mapper.Map<Test, TestBusiness>(myTask);
        }

        public async Task<List<TestBusiness>> GetTests()
        {
            var testListTask = await _ctx.Tests.Select(t => new TestBusiness()
            {
                Id = t.Id,
                Title = t.Title,
                URL = t.URL,
                Date = t.Date,
            }).ToListAsync();

            return testListTask;
        }

        public async Task DeleteTest(int testId)
        {
            var task = await _ctx.Tests.FirstOrDefaultAsync(t => t.Id == testId);

            if (task == null)
            {
                throw new Exception($"Test not found");
            }

            _ctx.Tests.Remove(task);
            _ctx.SaveChanges();
        }

        public async Task UpdateTest(TestBusiness test)
        {
            _ctx.Database.Log = x => Debug.WriteLine(x);
            var mapper = _kernel.Get<IMapper>("Heavy");
            var newValue = mapper.Map<TestBusiness, Test>(test);
            var oldValue = (EntityExtension)await(_ctx.Tests.FirstOrDefaultAsync(x => x.Id == test.Id));
            NewUpdate(newValue, oldValue);
            await _ctx.SaveChangesAsync();
        }

        public void NewUpdate(EntityExtension newValue, EntityExtension oldValue)
        {
            try
            {

                oldValue.SetFlatProperty(newValue);
                var toRemove = oldValue.Childs.Where(x => newValue.Childs.All(y => y.MyId != x.MyId)).ToList();
                var toAdd = newValue.Childs.Where(x => oldValue.Childs.All(y => y.MyId != x.MyId)).ToList();
                var toUpdate = newValue.Childs.Where(x => oldValue.Childs.Any(y => y.MyId == x.MyId)).ToList();

                foreach (var child in toRemove) 
                {
                    oldValue.RemoveChild(child, _ctx);
                    _ctx.Entry(child).State = EntityState.Deleted;
                }
                foreach (var child in toAdd) oldValue.AddChild(child, _ctx);

                foreach (var child in toUpdate)
                {
                    var childToUpdate = oldValue.Childs.FirstOrDefault(x => x.MyId == child.MyId);
                    NewUpdate(child, childToUpdate);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public async Task DeleteQuestionByIdFromTest(int questionId)
        {
            var question = await _ctx.Questions.FirstOrDefaultAsync(q => q.Id == questionId);

            if (question == null)
            {
                throw new NullReferenceException("Question not found ");
            }

            _ctx.Questions.Remove(question);
            _ctx.SaveChanges();
            await UpdateQuestionsPosition(question.TestId);
        }

        public void Dispose()
        {
            _ctx?.Dispose();
        }

        public async Task<QuestionBusiness> GetQuestionById(int id)
        {
            var taskQuestion = await _ctx.Questions.FirstOrDefaultAsync(q => q.Id == id);

            if (taskQuestion == null)
            {
                throw new NullReferenceException("Question not found");
            }

            var mapper = _kernel.Get<IMapper>("Heavy");
            var returned = mapper.Map<Question, QuestionBusiness>(taskQuestion);
            return returned;
        }

        public async Task UpdateQuestion(QuestionBusiness updateQuestion)
        {
            var newQuestion = (EntityExtension)QuestionMapper.MapBizToDal(updateQuestion);
            var oldQuestion = await _ctx.Questions.FirstOrDefaultAsync(q => q.Id == updateQuestion.Id);
            NewUpdate(newQuestion, oldQuestion);
            await _ctx.SaveChangesAsync();
        }

        public async Task<Dictionary<int, int>> GetExTestCountByState(IEnumerable<int> testsIds, AdministratedTestState state)
        {
            var returned = new Dictionary<int, int>();

            foreach (var id in testsIds)
            {
                var test = await _ctx.Tests.FirstOrDefaultAsync(t => t.Id == id);

                if (test == null)
                {
                    throw new NullReferenceException("Test not found");
                }

                returned.Add(id, test.AdministratedTests.Count(a => a.State == (byte)state));
            }

            return returned;
        }

        public async Task UpdateQuestionsPosition(int testId)
        {
            TestBusiness test = await GetTest(testId);
            short pos = 0;

            foreach (var question in test.Questions)
            {
                question.Position = pos;
                pos++;
            }

            await UpdateTest(test);
        }

        public async Task<int> GetExTestCountByState(int testId, AdministratedTestState state)
        {
            var test = await _ctx.Tests.FirstOrDefaultAsync(x => x.Id == testId);

            return test.AdministratedTests.Count(a => a.State == (byte)state);
        }
        public async Task<int> GetExTestCount(int testId)
        {
            var test = await _ctx.Tests.FirstOrDefaultAsync(x => x.Id == testId);
            return test.AdministratedTests.Count();
        }
        public async Task<List<TestBusiness>> GetAllFiltered(int pageNum, int pageSize, string filter)
        {

            return await _ctx.Tests
                            .OrderBy(x => x.Id)
                            .Where(test => test.Title.Contains(filter))
                            .Skip((pageNum - 1) * pageSize)
                            .Take(pageSize).Select(t => new TestBusiness()
                            {
                                Id = t.Id,
                                Title = t.Title,
                                URL = t.URL,
                                Date = t.Date,
                            })
                            .ToListAsync();
        }
        public async Task<List<TestBusiness>> GetAllFiltered(int pageNum, int pageSize)
        {
            return await _ctx.Tests
                            .OrderBy(x => x.Id)
                            .Skip((pageNum - 1) * pageSize)
                            .Take(pageSize).Select(t => new TestBusiness()
                            {
                                Id = t.Id,
                                Title = t.Title,
                                URL = t.URL,
                                Date = t.Date,
                            })
                            .ToListAsync();
        }

        async public Task<int> CountTests(string filter)
        {
            return await _ctx.Tests.Where(t => t.Title.Contains(filter)).CountAsync();
        }
        async public Task<int> CountTests()
        {
            return await _ctx.Tests.CountAsync();

        }

        public async Task<TestBusiness> GetTestLight(int testId)
        {
            var myTask = await _ctx.Tests.FirstOrDefaultAsync(t => t.Id == testId);

            if (myTask == null)
            {
                throw new Exception($"Test not found");
            }
            var mapper = _kernel.Get<IMapper>("Light");
            return mapper.Map<Test, TestBusiness>(myTask);
        }
    }
}
