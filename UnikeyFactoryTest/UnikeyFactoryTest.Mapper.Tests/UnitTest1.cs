using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using Ninject.Modules;
using UnikeyFactoryTest.Context;
using UnikeyFactoryTest.Domain;
using UnikeyFactoryTest.Mapper.AutoMappers;
using UnikeyFactoryTest.Mapper.AutoMappers.Attributes;

namespace UnikeyFactoryTest.Mapper.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void AnswerAutoMapper_OK()
        {
            var answer = new Answer
            {
                Id = 1,
                Text = "",
                IsCorrect = 0,
                Question = new Question
                {
                    Id = 1,
                    Text = "",
                    Answers = new List<Answer>(),
                    Test = new Test(),
                    TestId = 1
                },
                QuestionId = 1,
                Score = 1
            };

            var _kernel = new StandardKernel();
            _kernel.Bind<MapperConfiguration>().ToConstant(LightConfiguration.Configure()).WhenClassHas(typeof(LightAttribute));
            _kernel.Bind<MapperConfiguration>().ToConstant(HeavyConfiguration.Configure()).WhenClassHas(typeof(HeavyAttribute));

            var mapperLight = new AutoMapper.Mapper(LightConfiguration.Configure(), type => _kernel.Get(type));

            _kernel.Bind<IMapper>().ToMethod(ctx => mapperLight);
            //_kernel.Bind<IMapper>().ToMethod(ctx => new AutoMapper.Mapper(LightConfiguration.Configure(), type => ctx.Kernel.Get(type))).Named("Light");
            
            var mapper = _kernel.Get<IMapper>();
            var ab = mapper.Map<Answer, AnswerBusiness>(answer);
            Assert.AreEqual(1, ab.Question.Id);
        }
    }

    public static class HeavyConfiguration
    {
        public static MapperConfiguration Configure()
        {
            var mapperConfig = new MapperConfiguration(cfg => 
                cfg.AddProfiles(new List<Profile>
                {
                    new AnswerAutoMapper(),
                    new QuestionAutoMapper()
                }));

            return mapperConfig;
        }
    }

    public static class LightConfiguration
    {
        public static MapperConfiguration Configure()
        {
            var mapperConfig = new MapperConfiguration(cfg => 
                cfg.AddProfiles(new List<Profile>
                {
                    new AnswerAutoMapperLight(),
                    new QuestionAutoMapperLight()
                }));

            return mapperConfig;
        }
    }
}
