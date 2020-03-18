﻿using System;
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
            var key = new SymmetricSecurityKey(Encoding.Default.GetBytes("MeFaSchifoLAgile"));
            
            var token = jwtHandler.CreateJwtSecurityToken(
                "issuer",
                "Audience",
                new ClaimsIdentity(new List<Claim>
                {
                    new Claim("id", $"{claims[0]}"),
                    new Claim("userName", $"{claims[1]}")
                }),
                DateTime.Now,
                DateTime.Now.AddSeconds(60),
                DateTime.Now,
                new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );
            return token;
        }

        public static string RefreshToken(JwtSecurityToken jwt)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            try
            {
                var id = jwt.Claims.FirstOrDefault(c => c.Type.Equals("id")).Value;
                var userName = jwt.Claims.FirstOrDefault(c => c.Type.Equals("userName")).Value;
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