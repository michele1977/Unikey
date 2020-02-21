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
    public class UserAutomapperLight : Profile
    {
        public UserAutomapperLight()
        {
            CreateMap<UserBusiness, User>().ForMember(u => u.Tests, cfg => cfg.Ignore())
                .ForMember(u => u.Username, cfg => cfg.MapFrom(ub => ub.UserName));

            CreateMap<User, UserBusiness>().ForMember(ub => ub.Tests, cfg => cfg.Ignore())
                .ForMember(ub => ub.UserName, cfg => cfg.MapFrom(u => u.Username));
        }
    }
}
