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
    public class AdministratedAnswerAutoMapper : Profile
    {
        public AdministratedAnswerAutoMapper()
        {
            CreateMap<AdministratedAnswer, AdministratedAnswerBusiness>();
            CreateMap<AdministratedAnswerBusiness, AdministratedAnswer>();
        }
    }
}
