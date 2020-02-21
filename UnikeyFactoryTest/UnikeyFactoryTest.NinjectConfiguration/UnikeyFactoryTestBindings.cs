﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Ninject.Modules;
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
            Bind(typeof(IUser<>)).To<UserBusiness>();
            Bind<IUserRepository>().To<UserRepository>();
            Bind<IUserStore<UserBusiness, int>>().To<UserRepository>();
            Bind(typeof(UserManager<>)).To<UserService>();
        }
    }
}
