using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using AutoMapper;
using UnikeyFactoryTest.Context;
using UnikeyFactoryTest.Domain;

namespace UnikeyFactoryTest.Presentation
{
    public class MapConfig
    {
        public static void RegisterMap()
        {
            var userConfig = new MapperConfiguration(cfg => cfg.CreateMap<User, UserBusiness>()
                .ForMember(ub => ub.Tests, u => u.Ignore())
            );

            var testConfig = new MapperConfiguration(cfg => cfg.CreateMap<Test, TestBusiness>()
                .ForMember(tb => tb.Questions, t => t.Ignore())
                .ForMember(tb => tb.AdministratedTests, t => t.Ignore())
            );

            var questionConfig = new MapperConfiguration(cfg => cfg.CreateMap<Question, QuestionBusiness>()
                .ForMember(qb => qb.Answers, q => q.Ignore())
            );

            var answerConfig = new MapperConfiguration(cfg => cfg.CreateMap<Answer, AnswerBusiness>());

            var adTestConfig = new MapperConfiguration(cfg => cfg.CreateMap<AdministratedTest, AdministratedTestBusiness>()
                .ForMember(atb => atb.AdministratedQuestions, at => at.Ignore())
            );

            var adQuestConfig = new MapperConfiguration(cfg => cfg.CreateMap<AdministratedQuestion, AdministratedQuestionBusiness>()
                .ForMember(aqb => aqb.AdministratedAnswers, aq => aq.Ignore())
            );

            var adAnsConfig = new MapperConfiguration(cfg => cfg.CreateMap<AdministratedAnswer, AdministratedAnswerBusiness>()
                .ForMember(aab => aab.AdministratedQuestion, aa => aa.Ignore())
            );
        }
    }
}