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
    public class QuestionAutoMapperLight : Profile
    {
        public QuestionAutoMapperLight()
        {
            CreateMap<Question, QuestionBusiness>()
                .ForMember(ab => ab.Answers, a => a.Ignore());

            CreateMap<QuestionBusiness, Question>()
                .ForMember(a => a.Test, ab => ab.Ignore())
                .ForMember(a => a.Answers, ab => ab.Ignore());
        }
    }
}
