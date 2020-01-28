﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using AutoMapper;
using Ninject;
using Ninject.Infrastructure.Language;
using UnikeyFactoryTest.Context;
using UnikeyFactoryTest.Domain;
using UnikeyFactoryTest.Mapper;
using UnikeyFactoryTest.Mapper.AutoMappers;
using UnikeyFactoryTest.Mapper.AutoMappers.Attributes;

namespace UnikeyFactoryTest.Presentation
{
    public class MapConfig
    {
        public void RegisterMap(IKernel kernel)
        {
            kernel.Bind<MapperConfiguration>().ToConstant(Configure()).InSingletonScope();
            var mapper = new AutoMapper.Mapper(ConfigureLight(), type => kernel.Get(type));
            kernel.Bind<IMapper>().ToMethod(ctx => mapper);
        }

        private static MapperConfiguration Configure()
        {
            var mapperConfig = new MapperConfiguration(cfg => 
                cfg.AddProfiles(new List<Profile>
                {
                    new AnswerAutoMapper(),
                    new QuestionAutoMapper()
                }));

            return mapperConfig;
        }
        
        private static MapperConfiguration ConfigureLight()
        {
            var mapperConfig = new MapperConfiguration(cfg => 
                cfg.AddProfiles(new List<Profile>
                {
                    new AnswerAutoMapperLight()
                }));

            return mapperConfig;
        }
    }
}