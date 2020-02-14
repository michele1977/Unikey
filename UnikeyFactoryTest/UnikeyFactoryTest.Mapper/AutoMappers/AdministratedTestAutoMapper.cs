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
    public class AdministratedTestAutoMapper : Profile
    {
        public AdministratedTestAutoMapper()
        {
            CreateMap<AdministratedTest, AdministratedTestBusiness>()
                .ForMember(atb => atb.State, at => at.MapFrom(c => (int)c.State));
            CreateMap<AdministratedTestBusiness, AdministratedTest>()
                .ForMember(at => at.State, at => at.MapFrom(c => (byte)c.State));
        }
    }
}
