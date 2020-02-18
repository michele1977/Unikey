using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Modules;
using UnikeyFactoryTest.IRepository;
using UnikeyFactoryTest.IService;
using UnikeyFactoryTest.Repository;
using UnikeyFactoryTest.Service;

namespace UnikeyFactoryTest.NinjectConfiguration
{
    public class UnikeyFactoryTestBindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IAdministratedTestService>().To<AdministratedTestService>();
            Bind<IAdministratedTestRepository>().To<AdministratedTestRepository>();
            Bind<ITestRepository>().To<TestRepository>();
            Bind<ITestService>().To<TestService>();
            Bind<IUserRepository>().To<UserRepository>();
            Bind<IUserService>().To<UserService>();
        }
    }
}
