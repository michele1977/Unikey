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
    public class AnswerAutoMapperMedium : Profile
    {
        public AnswerAutoMapperMedium()
        {
            CreateMap<Answer, AnswerBusiness>();
            CreateMap<AnswerBusiness, Answer>();
        }
    }
}
