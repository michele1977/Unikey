using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Ninject;
using Ninject.Modules;
using Owin;
using UnikeyFactoryTest.Domain;
using UnikeyFactoryTest.IRepository;
using UnikeyFactoryTest.NinjectConfiguration;
using UnikeyFactoryTest.Service;

[assembly: OwinStartup(typeof(UnikeyFactoryTest.Presentation.Startup))]

namespace UnikeyFactoryTest.Presentation
{
    public class Startup
    {
        public IKernel Kernel { get; set; }
        public Startup()
        {
            Kernel = new StandardKernel();
            Kernel.Load(new List<INinjectModule>()
            {
                new AutoMapperBindingsService(),
                new UnikeyFactoryTestBindings()
            });
        }

        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext(() => Kernel.Get<UserManager<UserBusiness, int>>());
            app.CreatePerOwinContext(() => Kernel.Get<IUserRepository>());
            app.CreatePerOwinContext<SignInManager<UserBusiness, int>>((opt, ctx) =>
                new SignInManager<UserBusiness, int>(
                    ctx.Get<UserManager<UserBusiness, int>>(),
                    ctx.Authentication));
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                AuthenticationMode = AuthenticationMode.Active,
                ExpireTimeSpan = TimeSpan.FromMinutes(1),
                LoginPath = new PathString("/User/Index"),
            });
        }
    }
}
