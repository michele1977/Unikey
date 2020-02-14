using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace UnikeyFactoryTest.Mapper.AutoMappers
{
    public interface IAutoMapperFactory
    {
        IMapper GetMapper(string type);
    }
}
