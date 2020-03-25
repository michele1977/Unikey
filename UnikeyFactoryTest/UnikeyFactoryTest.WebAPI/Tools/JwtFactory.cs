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
            var jwtHandler = new JwtSecurityTokenHandler();
            var token = CreateJwtSecurityToken(jwtHandler, user.Id, user.UserName);
            
            return jwtHandler.WriteToken(token);
        }

        private static JwtSecurityToken CreateJwtSecurityToken(JwtSecurityTokenHandler jwtHandler, params object[] claims)
        {
            var key = new SymmetricSecurityKey(Encoding.Default.GetBytes("MeFaSchifoLAgileyufntdbrsve"));
            
            var token = jwtHandler.CreateJwtSecurityToken(
                "issuer",
                "audience",
                new ClaimsIdentity(new List<Claim>
                {
                    new Claim("id", $"{claims[0]}"),
                    new Claim("userName", $"{claims[1]}")
                }),
                DateTime.Now,
                DateTime.Now.AddHours(24),
                DateTime.Now,
                new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );
            return token;
        }

        public static string RefreshToken(string jwt)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            var myJwt =jwtHandler.ReadJwtToken(jwt);
            try
            {
                var id = myJwt.Claims.FirstOrDefault(c => c.Type.Equals("id")).Value;
                var userName = myJwt.Claims.FirstOrDefault(c => c.Type.Equals("userName")).Value;
                var newJwt = jwtHandler.WriteToken(CreateJwtSecurityToken(new JwtSecurityTokenHandler(), id, userName));
                return newJwt;
            }
            catch (ArgumentNullException e)
            {
                return e.Message;
            }
        }
    }
}