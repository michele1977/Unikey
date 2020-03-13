using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Jwt;
using Ninject;
using Ninject.Modules;
using Owin;
using UnikeyFactoryTest.Domain;
using UnikeyFactoryTest.IRepository;
using UnikeyFactoryTest.NinjectConfiguration;
using UnikeyFactoryTest.Service;

[assembly: OwinStartup(typeof(UnikeyFactoryTest.WebAPI.Startup))]

namespace UnikeyFactoryTest.WebAPI
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

            app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ExternalBearer,
                AuthenticationMode = AuthenticationMode.Active
            });
        }
    }
}
