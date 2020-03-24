using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UnikeyFactoryTest.Domain;

namespace UnikeyFactoryTest.WebAPI.Tools
{
    public class CustomJwtFormat : ISecureDataFormat<AuthenticationTicket>
    {
        public CustomJwtFormat(string issuer)
        {
        }

        public string Protect(AuthenticationTicket data)
        {
            var user = new UserBusiness
            {
                Id = Convert.ToInt32(data.Identity.Claims.FirstOrDefault(c => c.Type.Equals("id")).Value),
                UserName = data.Identity.Claims.FirstOrDefault(c => c.Type.Equals("userName")).Value
            };

            var jwt = JwtFactory.GenerateToken(user);
            return jwt;
        }
        

        public AuthenticationTicket Unprotect(string protectedText)
        {
            throw new NotImplementedException();
        }

    }
}