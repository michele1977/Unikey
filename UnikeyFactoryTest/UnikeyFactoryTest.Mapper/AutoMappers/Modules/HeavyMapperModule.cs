using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Ninject;
using Ninject.Modules;

namespace UnikeyFactoryTest.Mapper.AutoMappers.Modules
{
    public class HeavyMapperModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<MapperConfiguration>().ToConstant(Configure()).InSingletonScope();
            Kernel.Bind<IMapper>().ToMethod(ctx => new AutoMapper.Mapper(ConfigureLight(), type => Kernel.Get(type)));
        }

        #region Configure()
        private static MapperConfiguration Configure()
        {
            var mapperConfig = new MapperConfiguration(cfg => 
                cfg.AddProfiles(new List<Profile>
                {
                    new AnswerAutoMapper(),
                    new QuestionAutoMapper()
                }));

            return mapperConfig;
        }
        #endregion

        #region ConfigureLight()
        private static MapperConfiguration ConfigureLight()
        {
            var mapperConfig = new MapperConfiguration(cfg => 
                cfg.AddProfiles(new List<Profile>
                {
                    new AnswerAutoMapperLight()
                }));

            return mapperConfig;
        }
        #endregion



    }
}
