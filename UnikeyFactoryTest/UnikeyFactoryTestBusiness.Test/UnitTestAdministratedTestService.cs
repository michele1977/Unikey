using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnikeyFactoryTest.Context;
using UnikeyFactoryTest.Domain;
using UnikeyFactoryTest.Mapper;
using UnikeyFactoryTest.Repository;
using UnikeyFactoryTest.Service;

namespace UnikeyFactoryTestBusiness.Test
{
    [TestClass]
    public class UnitTestAdministratedTestService
    {
        public TestPlatformDBEntities Ctx { get; set; }

        private TestBusiness _test = new TestBusiness()
        {
            Id = 1,
            URL = "localhost:1234/boh",
            Date = DateTime.Now,
            AdministratedTests = new List<AdministratedTestBusiness>(),
            Questions = new List<QuestionBusiness>()
            {
                new QuestionBusiness
                {
                    Id = 1,
                    Text = "Boh?",
                    Answers = new List<AnswerBusiness>()
                    {
                        new AnswerBusiness()
                        {
                            Id = 1,
                            IsCorrect = true,
                            Score = 10,
                            Text = "Bah"
                        },
                        new AnswerBusiness()
                        {
                            Id = 2,
                            IsCorrect = false,
                            Score = 0,
                            Text = "Beh"
                        },
                        new AnswerBusiness()
                        {
                            Id = 3,
                            IsCorrect = false,
                            Score = 0,
                            Text = "Mah"
                        }
                    }
                }
            }
        };

        [TestMethod]
        public void AdministratedTestMapper_OK()
        {
            Ctx = new TestPlatformDBEntities();
            Ctx.Tests.Add(TestMapper.MapBizToDal(_test));
            AdministratedTestRepository repo = new AdministratedTestRepository(Ctx);
            AdministratedTestService administratedTestService = new AdministratedTestService(repo);
            var adTest = administratedTestService.AdministratedTest_Builder(TestMapper.MapDalToBiz(Ctx.Tests.Find(1)), "Daniele Tulli");

            var dao = AdministratedTestMapper.MapDomainToDao(adTest);

            Assert.AreEqual(dao, adTest);
        }

        [TestMethod]
        public void AdministratedTest_Builder_OK()
        {
            Ctx = new TestPlatformDBEntities();
            Ctx.Tests.Add(TestMapper.MapBizToDal(_test));
            AdministratedTestRepository repo = new AdministratedTestRepository(Ctx);
            AdministratedTestService administratedTestService = new AdministratedTestService(repo);
            var adTest = administratedTestService.AdministratedTest_Builder(TestMapper.MapDalToBiz(Ctx.Tests.Find(1)), "Daniele Tulli");

            adTest.AdministratedQuestions.Should()
                .NotBeNull()
                .And
                .HaveCount(1);
        }

        [TestMethod]
        public void Add_OK()
        {
            Ctx = new TestPlatformDBEntities();
            Ctx.Tests.Add(TestMapper.MapBizToDal(_test));
            AdministratedTestRepository repo = new AdministratedTestRepository(Ctx);
            AdministratedTestService administratedTestService = new AdministratedTestService(repo);
            var adTest = administratedTestService.AdministratedTest_Builder(TestMapper.MapDalToBiz(Ctx.Tests.Find(1)), "Daniele Tulli");

            administratedTestService.Add(adTest);

            var ad = repo.GetAdministratedTestById(1);

            ad.Should()
                .NotBeNull()
                .And
                .Be(ad.Id == 1);
        }

        [TestMethod]
        public void Update_Save_OK()
        {
        }

        [TestMethod]
        public void GetAdministratedTestById_OK()
        {
        }
    }
}
