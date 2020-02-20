﻿using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Ninject;
using UnikeyFactoryTest.Context;
using UnikeyFactoryTest.Domain;
using UnikeyFactoryTest.IRepository;
using UnikeyFactoryTest.Mapper;

namespace UnikeyFactoryTest.Repository
{
    public class TestRepository : ITestRepository
    {
        private readonly TestPlatformDBEntities _ctx;
        private IKernel Kernel;
        public TestRepository()
        {
            _ctx = new TestPlatformDBEntities();
        }

        public TestRepository(TestPlatformDBEntities myCtx,IKernel kernel)
        {
            Kernel = kernel;
            _ctx = myCtx;
        }

        public async Task SaveTest(Test test)
        {
            var myTask = Task.Run(() =>
            {
                _ctx.Tests.Add(test);
                _ctx.SaveChanges();
            });
            await myTask;
        }


        public async Task<TestBusiness> GetTestByURL(string URL)
        {
            var result = await _ctx.Tests.FirstAsync(x => x.URL.Equals(URL));

            if (result == null)
            {
                throw new Exception($"Test not found at specified URL ({URL})");
            }

            var mapper = Kernel.Get<IMapper>("Heavy");
            return mapper.Map<Test, TestBusiness>(result);
        }

        public async Task<TestBusiness> GetTest(int testId)
        {
            var myTask = await Task.Run(() => _ctx.Tests.First(t => t.Id == testId));

            if (myTask == null)
            {
                throw new Exception($"Test not found at specified id ({testId})");
            }
            var mapper = Kernel.Get<IMapper>("Heavy");
            return mapper.Map<Test, TestBusiness>(myTask);
        }

        public async Task<List<TestBusiness>> GetTests()
        {
            var returned = _ctx.Tests.ToList();

            var testListTask = await Task.Run(() => _ctx.Tests.Select(t => new TestBusiness()
            {
                Id = t.Id,
                Title = t.Title,
                URL = t.URL,
                Date = t.Date,
            }).ToList());

            return testListTask;
        }

        public async Task DeleteTest(int testId)
        {
            var task = await Task.Run(() => { return _ctx.Tests.First(t => t.Id == testId); });

            _ctx.Tests.Remove(task);
            _ctx.SaveChanges();
        }


        //public async void UpdateTest(TestBusiness test)
        //{
        //    if (test == null)
        //    {
        //        throw new NullReferenceException("No test to update");
        //    }
        //    var newValue = (EntityExtension)TestMapper.MapBizToDal(test);
        //    var oldValue = await Task.Run(() => (EntityExtension)(_ctx.Tests.FirstOrDefault(x => x.Id == newValue.MyId))); 
        //    NewUpdate(newValue, oldValue);
        //}

        public void UpdateTest(TestBusiness test)
        {
            var mapper = Kernel.Get<IMapper>("Heavy");
            var newValue = mapper.Map<TestBusiness, Test>(test);
            var oldValue = (EntityExtension) (_ctx.Tests.FirstOrDefault(x => x.Id == test.Id));
            NewUpdate(newValue, oldValue);
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
        public async Task DeleteQuestionByIdFromTest(int questionId)
        {
            var question = await Task.Run(() => { return _ctx.Questions.FirstOrDefault(q => q.Id == questionId); });
            if (question == null)
            {
                throw new NullReferenceException("Question not found ");
            }

            _ctx.Questions.Remove(question);
            _ctx.SaveChanges();
        }

        public void Dispose()
        {
            _ctx.Dispose();
        }

        public async Task<QuestionBusiness> GetQuestionById(int id)
        {
            var taskQuestion = await Task.Run(() => 
            {
                return _ctx.Questions.FirstOrDefault(q => q.Id == id);
            });
            var mapper = Kernel.Get<IMapper>("Heavy");
            var returned = mapper.Map<Question, QuestionBusiness>(taskQuestion);
            return returned;
        }

        public async Task<Dictionary<int, int>> OpenedTestNumber(IEnumerable<int> TestsId)
        {
            var result = await Task.Run(() =>
            {
                var returned = new Dictionary<int, int>();
                foreach (var Id in TestsId)
                {
                    var test = _ctx.Tests.FirstOrDefault(t => t.Id == Id);
                    if (test != null)
                    {
                        returned.Add(Id, test.AdministratedTests.Count(a => a.State == 1));
                    }
                    else throw new Exception("Test not found");
                }
                return returned;
            });

            return result;
        }


        public Dictionary<int,int> GetClosedTests(int pageNum, int pageSize)
        {
            var testIdList = _ctx.Tests.OrderBy(t => t.Id).Skip((pageNum - 1) * pageSize).Take(pageSize).Select(t => t.Id).ToList();

            Dictionary<int, int> numClosedAdTestsDictionary= new Dictionary<int, int>();

            foreach (var id in testIdList)
            {
                var numClosedTests = _ctx.AdministratedTests.Count(adT => adT.TestId == id && adT.State == 3);

                numClosedAdTestsDictionary.Add(id, numClosedTests);

            }

            return numClosedAdTestsDictionary;
        }
    }
}
