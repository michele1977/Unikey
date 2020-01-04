﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnikeyFactoryTest.Context;
using UnikeyFactoryTest.Domain;

namespace UnikeyFactoryTest.Mapper
{
    public static class UserMapper
    {
        public static UserBusiness MapDalToBiz(User user)
        {
            var returned = new UserBusiness()
            {
                Id = user.Id,
                Tests = user.Tests.Select(TestMapper.MapDalToBiz),
                Password = user.Password,
                Username = user.Username
            };

            return returned;
        }

        public static User MapBizToDal(UserBusiness user)
        {
            var returned = new User()
            {
                Id = user.Id,
                Tests = user.Tests.Select(TestMapper.MapBizToDal).ToList(),
                Password = user.Password,
                Username = user.Username
            };

            return returned;
        }
    }
}
