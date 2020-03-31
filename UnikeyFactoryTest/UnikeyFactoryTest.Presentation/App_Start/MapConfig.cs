using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using AutoMapper;
using Ninject;
using Ninject.Infrastructure.Language;
using UnikeyFactoryTest.Context;
using UnikeyFactoryTest.Domain;
using UnikeyFactoryTest.Mapper;
using UnikeyFactoryTest.Mapper.AutoMappers;

namespace UnikeyFactoryTest.Presentation
{
    public class MapConfig
    {
        public void RegisterMap(IKernel kernel)
        {
            kernel.Bind<MapperConfiguration>().ToConstant(ConfigureLight()).InSingletonScope();
            kernel.Bind<MapperConfiguration>().ToConstant(Configure()).InSingletonScope();
            kernel.Bind<IKernel>().To<StandardKernel>().InSingletonScope();
            kernel.Bind<IAutoMapperFactory>().To<AutoMapperFactory>();

            kernel.Bind<IMapper>().ToMethod(ctx => new AutoMapper.Mapper(ConfigureLight(), type => kernel.Get(type)))
                .Named("Light");
            kernel.Bind<IMapper>().ToMethod(ctx => new AutoMapper.Mapper(ConfigureLight(), type => kernel.Get(type)))
                .Named("Medium");
            kernel.Bind<IMapper>().ToMethod(ctx => new AutoMapper.Mapper(Configure(), type => kernel.Get(type)))
                .Named("Heavy");
        }

        private static MapperConfiguration Configure()
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
        
        private static MapperConfiguration ConfigureLight()
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
        private static MapperConfiguration ConfigureMedium()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
                cfg.AddProfiles(new List<Profile>
                {
                    new AnswerAutoMapperMedium(),
                    new QuestionAutoMapperMedium(),
                    new TestAutoMapperMedium()
                }));

            return mapperConfig;
        }
    }
}