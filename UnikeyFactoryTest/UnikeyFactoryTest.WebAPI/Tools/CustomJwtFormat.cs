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
                Id = data.Identity.NameClaimType[""]
            }
            var jwt = JwtFactory.GenerateToken();
            return jwt;
        }
        

        public AuthenticationTicket Unprotect(string protectedText)
        {
            throw new NotImplementedException();
        }

    }
}