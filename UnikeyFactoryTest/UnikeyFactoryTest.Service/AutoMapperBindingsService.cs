using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Ninject.Modules;
using UnikeyFactoryTest.IRepository;
using UnikeyFactoryTest.IService;
using UnikeyFactoryTest.Mapper.AutoMappers;
using UnikeyFactoryTest.Repository;

namespace UnikeyFactoryTest.Service
{
    public class AutoMapperBindingsService : NinjectModule
    {
        public override void Load()
        {
            if (Kernel == null) return;
            Kernel.Bind<MapperConfiguration>().ToConstant(ModulesMapping.GetConfiguration());

            Kernel.Bind<IMapper>().ToMethod(ctx =>
                new global::AutoMapper.Mapper(ModuleMapping.GetConfiguration(),
                    type => Kernel.GetType())).Named("Heavy");

            Kernel.Bind<IMapper>().ToMethod(ctx =>
                new global::AutoMapper.Mapper(ModuleMapping.GetConfigurationLight(),
                    type => Kernel.GetType())).Named("Light");
        }
    }
}
