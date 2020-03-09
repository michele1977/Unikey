using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;
using Microsoft.IdentityModel.Tokens;
using UnikeyFactoryTest.Domain;

namespace UnikeyFactoryTest.WebAPI.Tools
{
    public static class JwtFactory
    {
        public static string GenerateToken(UserBusiness user)
        {
            var key = new SymmetricSecurityKey(Encoding.Default.GetBytes("MeFaSchifoLAgile"));

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
    }
}