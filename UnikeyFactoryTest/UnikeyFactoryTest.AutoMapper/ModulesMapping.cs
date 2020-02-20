using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnikeyFactoryTest.Mapper.AutoMappers;

namespace UnikeyFactoryTest.AutoMapper
{
    public class ModulesMapping
    {
        private IMapper Mapper { get; set; }

        public ModulesMapping(IMapper mapper)
        {
            Mapper = mapper;
        }
        public ModulesMapping()
        {

        }
        public static MapperConfiguration GetConfiguration()
        {
            return Configure();
        }
        private static MapperConfiguration Configure()
        {
            var ModelConfig = new MapperConfiguration(cfg => cfg.AddProfiles(
                new List<Profile>
                {
                    new AnswerAutoMapper(),
                    new QuestionAutoMapper(),
                    new TestAutoMapper(),
                    new AdministratedTestAutoMapper(),
                    new AdministratedQuestionAutoMapper(),
                    new AdministratedAnswerAutoMapper()
                }));
            return ModelConfig;
        }
    }
}
