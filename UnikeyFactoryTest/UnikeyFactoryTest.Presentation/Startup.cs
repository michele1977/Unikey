using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Ninject;
using Owin;
using UnikeyFactoryTest.Domain;
using UnikeyFactoryTest.IRepository;
using UnikeyFactoryTest.Repository;

[assembly: OwinStartup(typeof(UnikeyFactoryTest.Presentation.Startup))]

namespace UnikeyFactoryTest.Presentation
{
    public class Startup
    {
        public IKernel Kernel { get; set; } = new StandardKernel();

        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext(() => Kernel.Get<UserManager<UserBusiness, int>>());
            app.CreatePerOwinContext(() => Kernel.Get<IUserRepository>());
            app.CreatePerOwinContext<SignInManager<UserBusiness, int>>((opt, ctx) =>
                new SignInManager<UserBusiness, int>(
                    ctx.Get<UserManager<UserBusiness,int>>(),
                    ctx.Authentication));
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie
            });
        }
    }
}
