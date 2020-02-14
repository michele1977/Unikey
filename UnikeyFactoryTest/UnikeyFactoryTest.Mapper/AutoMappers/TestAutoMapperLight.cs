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
    public class TestAutoMapperLight : Profile
    {
        public TestAutoMapperLight()
        {
            CreateMap<Test, TestBusiness>()
                .ForMember(tb => tb.Questions, t => t.Ignore())
                .ForMember(tb => tb.AdministratedTests, t => t.Ignore());

            CreateMap<TestBusiness, Test>()
                .ForMember(t => t.Questions, tb => tb.Ignore())
                .ForMember(t => t.AdministratedTests, tb => tb.Ignore());
        }
    }
}
