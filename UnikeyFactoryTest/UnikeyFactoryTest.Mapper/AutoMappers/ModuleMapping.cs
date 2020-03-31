using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace UnikeyFactoryTest.Mapper.AutoMappers
{
    public class ModuleMapping
    {
        private IMapper Mapper { get; set; }

        public ModuleMapping(IMapper mapper)
        {
            Mapper = mapper;
        }
        public ModuleMapping()
        {

        }
        public static MapperConfiguration GetConfiguration()
        {
            return Configure();
        }
        public static MapperConfiguration GetConfigurationLight()
        {
            return ConfigureLight();
        }
        public static MapperConfiguration GetConfigurationMedium()
        {
            return ConfigureMedium();
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
                    new AdministratedAnswerAutoMapper(),
                    new UserAutoMapper()
                }));
            return ModelConfig;
        }
        private static MapperConfiguration ConfigureLight()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
                cfg.AddProfiles(new List<Profile>
                {
                    new AnswerAutoMapperLight(),
                    new QuestionAutoMapperLight(),
                    new TestAutoMapperLight(),
                    new AdministratedTestAutoMapperLight(),
                    new AdministratedQuestionAutoMapperLight(),
                    new AdministratedAnswerAutoMapperLight(),
                    new UserAutomapperLight()
                }));

            return mapperConfig;
        }
        private static MapperConfiguration ConfigureMedium()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
                cfg.AddProfiles(new List<Profile>
                {
                    new TestAutoMapperMedium(),
                    new QuestionAutoMapperMedium(),
                    new AnswerAutoMapperMedium()
                }));

            return mapperConfig;
        }
    }
}
