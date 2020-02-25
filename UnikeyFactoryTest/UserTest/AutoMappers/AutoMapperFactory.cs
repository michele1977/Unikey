using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Ninject;
using Ninject.Modules;

namespace UnikeyFactoryTest.Mapper.AutoMappers
{
    public class AutoMapperFactory : IAutoMapperFactory
    {
        public static IKernel Kernel{ get; set; }

        public AutoMapperFactory(IKernel kernel)
        {
            Kernel = kernel;
        }

        public IMapper GetMapper(string type)
        {
            var mapper = Kernel.Get<IMapper>(type);
            return mapper;
        }
    }
}
