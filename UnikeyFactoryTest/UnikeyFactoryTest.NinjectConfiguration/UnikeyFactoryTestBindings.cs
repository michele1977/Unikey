using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Ninject.Modules;
using UnikeyFactoryTest.Context;
using UnikeyFactoryTest.Domain;
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
            Bind<IUser<int>>().To<UserBusiness>();
            Bind<DbContext>().To<TestPlatformDBEntities>();
            Bind<IUserRepository>().To<UserRepository>();
            Bind<UserManager<UserBusiness, int>>().To<UserService>();
        }
    }
}
