using AutoMapper;
using Ninject;
using UnikeyFactoryTest.Context;
using UnikeyFactoryTest.Domain;

namespace UnikeyFactoryTest.Mapper.AutoMappers
{
    public class AnswerAutoMapperLight : Profile
    {
        public AnswerAutoMapperLight()
        {
            CreateMap<Answer, AnswerBusiness>();
            CreateMap<AnswerBusiness, Answer>().ForMember(a => a.Question, ab => ab.Ignore());
        }
    }
}
