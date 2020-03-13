using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnikeyFactoryTest.Domain;
using UnikeyFactoryTest.WebAPI.CustomAttributes;

namespace UserTest
{
    [TestClass]
    public class UnikeyFactoryTestAuthorization
    {
        public static string GenerateToken(UserBusiness user)
        {
            var key = new SymmetricSecurityKey(Encoding.Default.GetBytes("Bellavnaejbvoiòwbvorqin"));

            var jwtHandler = new JwtSecurityTokenHandler();
            var token = jwtHandler.CreateJwtSecurityToken(
                "issuer",
                "Audience",
                new ClaimsIdentity(new List<Claim>
                {
                    new Claim("int", $"{user.Id}"),
                    new Claim("int", $"{user.UserName}")
                }),
                DateTime.Now,
                DateTime.Now.AddHours(1),
                DateTime.Now,
                new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );
            
            return jwtHandler.WriteToken(token);
        }

        [TestMethod]
        public void LoginAuthorize_OK()
        {
            var jwt = GenerateToken(new UserBusiness
            {
                Id = 1,
                UserName = "pipposowlo"
            });
            
            var jwtHandler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = jwtHandler.ReadJwtToken(jwt);

            var result = LoginAuthorizeAttribute.CheckClaims(jwtSecurityToken) &&
                          LoginAuthorizeAttribute.CheckJwtStructure(jwt) &&
                          LoginAuthorizeAttribute.CheckSignature(jwtSecurityToken);

            result.Should().BeTrue();
        }
    }
}
