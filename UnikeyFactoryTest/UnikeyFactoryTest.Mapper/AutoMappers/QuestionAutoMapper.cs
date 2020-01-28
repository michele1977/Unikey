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
    public class QuestionAutoMapper : Profile
    {
        public QuestionAutoMapper()
        {
            CreateMap<Question, QuestionBusiness>();
            CreateMap<QuestionBusiness, Question>();
        }
    }
}
