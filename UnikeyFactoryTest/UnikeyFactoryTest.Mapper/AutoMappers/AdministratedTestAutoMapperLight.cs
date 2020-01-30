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
    public class AdministratedTestAutoMapperLight : Profile
    {
        public AdministratedTestAutoMapperLight()
        {
            CreateMap<AdministratedTest, AdministratedTestBusiness>()
                .ForMember(atb => atb.AdministratedQuestions, at => at.Ignore())
                .ForMember(atb => atb.State, at => at.Ignore());
            CreateMap<AdministratedTestBusiness, AdministratedTest>()
                .ForMember(atb => atb.AdministratedQuestions, at => at.Ignore())
                .ForMember(at => at.State, atb => atb.Ignore());
        }
    }
}
