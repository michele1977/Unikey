using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using UnikeyFactoryTest.Context;
using UnikeyFactoryTest.Domain;

namespace UnikeyFactoryTest.Mapper.AutoMappers
{
    public class TestAutoMapperMedium : Profile
    {
        public TestAutoMapperMedium()
        {
            CreateMap<Test, TestBusiness>()
                .ForMember(tb => tb.AdministratedTests, t => t.Ignore());

            CreateMap<TestBusiness, Test>()
                .ForMember(t => t.AdministratedTests, tb => tb.Ignore());
        }
    }
}
