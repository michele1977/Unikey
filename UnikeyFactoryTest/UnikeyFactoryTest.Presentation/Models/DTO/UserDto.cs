using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UnikeyFactoryTest.Domain;

namespace UnikeyFactoryTest.Presentation.Models.DTO
{
    public class UserDto
    {
        public UserDto()
        {

        }

        public UserDto(UserBusiness user)
        {
            Id = user.Id;
            Username = user.Username;
            Password = user.Password;
            Tests = user.Tests.Select(t => new TestDto(t));
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public IEnumerable<TestDto> Tests { get; set; }
    }
}