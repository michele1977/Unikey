using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
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
            HttpConfiguration config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();

            app.UseWebApi(config);
            
            app.CreatePerOwinContext(() => Kernel.Get<UserManager<UserBusiness, int>>());
            app.CreatePerOwinContext(() => Kernel.Get<IUserRepository>());

            app.CreatePerOwinContext<SignInManager<UserBusiness, int>>((opt, ctx) =>
                new SignInManager<UserBusiness, int>(
                    ctx.Get<UserManager<UserBusiness, int>>(),
                    ctx.Authentication));

            var base64Key = TextEncodings.Base64Url.Encode(Encoding.Default.GetBytes("MeFaSchifoLAgileyufntdbrsve"));
            
            app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ExternalBearer,
                AuthenticationMode = AuthenticationMode.Active,
                AllowedAudiences = new List<string>{ "audience" },
                IssuerSecurityKeyProviders = new IIssuerSecurityKeyProvider[]
                {
                    new SymmetricKeyIssuerSecurityKeyProvider("issuer", base64Key)
                }
            });
        }
    }
}
