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
    public class UserAutoMapper : Profile
    {
        public UserAutoMapper()
        {
            CreateMap<UserBusiness, User>().ForMember(u => u.Username, cfg => cfg.MapFrom(ub => ub.UserName));
            CreateMap<User, UserBusiness>().ForMember(ub => ub.UserName, cfg => cfg.MapFrom(u => u.Username));
        }
    }
}
