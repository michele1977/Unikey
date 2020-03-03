using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using AutoMapper;
using UnikeyFactoryTest.Mapper.AutoMappers;

namespace UnikeyFactoryTest.NinjectConfiguration
{
    public class MapConfig
    {
        public void RegisterMap(IKernel kernel)
        {
            kernel.Bind<MapperConfiguration>().ToConstant(ConfigureLight()).InSingletonScope();
            kernel.Bind<MapperConfiguration>().ToConstant(Configure()).InSingletonScope();
            //kernel.Bind<IKernel>().To<StandardKernel>().InSingletonScope();
            kernel.Bind<IAutoMapperFactory>().To<AutoMapperFactory>();

            kernel.Bind<IMapper>().ToMethod(ctx => new AutoMapper.Mapper(ConfigureLight(), type => kernel.Get(type)))
                .Named("Light");
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
                    new AdministratedAnswerAutoMapper(),
                    new UserAutoMapper()
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
                    new AdministratedAnswerAutoMapperLight(),
                    new UserAutomapperLight()
                }));

            return mapperConfig;
        }
    }
}

