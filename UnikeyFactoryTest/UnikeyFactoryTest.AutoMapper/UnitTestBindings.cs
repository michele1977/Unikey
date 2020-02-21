using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using UnikeyFactoryTest.IRepository;
using UnikeyFactoryTest.IService;
using UnikeyFactoryTest.Repository;
using UnikeyFactoryTest.Service;

namespace UnikeyFactoryTest.AutoMapper
{
    public class UnitTestBindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IAdministratedTestService>().To<AdministratedTestService>();
            Bind<IAdministratedTestRepository>().To<AdministratedTestRepository>();
            Bind<ITestRepository>().To<TestRepository>();
            Bind<ITestService>().To<TestService>();
            Bind<IUserRepository>().To<UserRepository>();
            Bind<IUserService>().To<UserService>();
            if (Kernel == null) return;
            Kernel.Bind<MapperConfiguration>().ToConstant(ModulesMapping.GetConfiguration());
            Kernel.Bind<IMapper>().ToMethod(ctx =>
                new global::AutoMapper.Mapper(ModulesMapping.GetConfiguration(), type => Kernel.GetType()));
        }
    }
}
