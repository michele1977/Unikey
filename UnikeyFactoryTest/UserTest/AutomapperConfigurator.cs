using Ninject;
using Ninject.Web.Common.WebHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnikeyFactoryTest.Presentation;
using UnikeyFactoryTest.Service;

namespace UserTest
{
    public class AutomapperConfigurator : NinjectHttpApplication
    {
        public void Configure()
        {
            var mapConfig = new MapConfig();
            mapConfig.RegisterMap(CreateKernel());
        }
        protected override IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            RegisterServices(kernel);
            return kernel;
        }
        private void RegisterServices(IKernel kernel)
        {
            //kernel.Load(new BindingsService());
        }
    }
}
