using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using Ninject.Modules;
using UnikeyFactoryTest.Context;
using UnikeyFactoryTest.Domain;
using UnikeyFactoryTest.Domain.Enums;
using UnikeyFactoryTest.Mapper.AutoMappers;

namespace UnikeyFactoryTest.Mapper.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void AnswerAutoMapper_OK()
        {
            var administratedTestBusiness = new AdministratedTestBusiness()
            {
                Id = 1,
                AdministratedQuestions = new List<AdministratedQuestionBusiness>
                {
                    new AdministratedQuestionBusiness
                    {
                        Id = 1,
                        AdministratedAnswers = new List<AdministratedAnswerBusiness>
                        {
                            new AdministratedAnswerBusiness
                            {
                                Id = 1
                            }
                        }
                    }
                },
                State = AdministratedTestState.Open,
                Test = new TestBusiness
                {
                    Id = 1
                }
            };
            var test = new Test
            {
                Id = 1,
                URL = "",
                Date = DateTime.Now,
                Title = "",
                AdministratedTests = new List<AdministratedTest>
                {
                    new AdministratedTest()
                }
            };

            var _kernel = new StandardKernel();
            _kernel.Bind<MapperConfiguration>().ToConstant(ConfigureLight()).InSingletonScope();
            _kernel.Bind<MapperConfiguration>().ToConstant(Configure()).InSingletonScope();
            
            _kernel.Bind<IMapper>().ToMethod(ctx => new AutoMapper.Mapper(ConfigureLight(), type => _kernel.Get(type)))
                .Named("Light");
            _kernel.Bind<IMapper>().ToMethod(ctx => new AutoMapper.Mapper(Configure(), type => _kernel.Get(type)))
                .Named("Heavy");

            var autoMapperFactory = new AutoMapperFactory(_kernel);
            var mapper = autoMapperFactory.GetMapper(test.AdministratedTests != null ? "Heavy" : "Light");

            var atb = mapper.Map<TestBusiness>(test);
            Assert.AreEqual(1, atb.Id);
        }

        public static MapperConfiguration Configure()
        {
            var mapperConfig = new MapperConfiguration(cfg => 
                cfg.AddProfiles(new List<Profile>
                {
                    new AnswerAutoMapper(),
                    new QuestionAutoMapper(),
                    new TestAutoMapper(),
                    new AdministratedTestAutoMapper(),
                    new AdministratedQuestionAutoMapper(),
                    new AdministratedAnswerAutoMapper()
                }));

            return mapperConfig;
        }

        public static MapperConfiguration ConfigureLight()
        {
            var mapperConfig = new MapperConfiguration(cfg => 
                cfg.AddProfiles(new List<Profile>
                {
                    new AnswerAutoMapperLight(),
                    new QuestionAutoMapperLight(),
                    new TestAutoMapperLight(),
                    new AdministratedTestAutoMapperLight(),
                    new AdministratedQuestionAutoMapperLight(),
                    new AdministratedAnswerAutoMapperLight()
                }));

            return mapperConfig;
        }
    }
}
