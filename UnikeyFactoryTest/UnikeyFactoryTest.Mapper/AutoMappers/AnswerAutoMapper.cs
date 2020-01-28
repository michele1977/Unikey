using System;
using AutoMapper;
using UnikeyFactoryTest.Context;
using UnikeyFactoryTest.Domain;
using UnikeyFactoryTest.Mapper.AutoMappers.Attributes;

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
