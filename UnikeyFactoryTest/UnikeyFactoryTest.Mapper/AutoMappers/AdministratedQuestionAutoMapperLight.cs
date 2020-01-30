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
    public class AdministratedQuestionAutoMapperLight : Profile
    {
        public AdministratedQuestionAutoMapperLight()
        {
            CreateMap<AdministratedQuestion, AdministratedQuestionBusiness>().ForMember(aqb => aqb.AdministratedAnswers, aq => aq.Ignore());
            CreateMap<AdministratedQuestionBusiness, AdministratedQuestion>().ForMember(aq => aq.AdministratedAnswers, aqb => aqb.Ignore());
        }
    }
}
