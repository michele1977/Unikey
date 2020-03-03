using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject;
using Ninject.Web.WebApi;

namespace UnikeyFactoryTest.WebAPI.App_Start
{
    [Serializable]
    public class CustomNinjectDependencyResolver : NinjectDependencyResolver
    {
        public CustomNinjectDependencyResolver(IKernel kernel) : base(kernel)
        {
        }
    }
}