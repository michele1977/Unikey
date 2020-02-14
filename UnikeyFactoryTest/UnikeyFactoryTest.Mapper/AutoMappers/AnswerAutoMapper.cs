using System;
using AutoMapper;
using UnikeyFactoryTest.Context;
using UnikeyFactoryTest.Domain;

namespace UnikeyFactoryTest.Mapper.AutoMappers
{
    public class AnswerAutoMapper : Profile
    {
        public AnswerAutoMapper()
        {
            CreateMap<Answer, AnswerBusiness>();
            CreateMap<AnswerBusiness, Answer>();
        }
    }
}
