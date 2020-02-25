using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnikeyFactoryTest.Service;

namespace UserTest.AutoMappers
{
    public static class KernelBuilder
    {
        public static StandardKernel Build()
        {
            var kernel = new StandardKernel();
            kernel.Load(new BindingsService());
            return kernel;
        }
    }
}
