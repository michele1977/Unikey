using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using UnikeyFactoryTest.WebAPI.Tools;

namespace UnikeyFactoryTest.WebAPI.CustomAttributes
{
    public class RefreshAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var jwt = actionContext.Request.Headers.Authorization.Parameter;
            var jwtHandler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = jwtHandler.ReadJwtToken(jwt);

            if (!CheckClaims(jwtSecurityToken))
                JwtFactory.RefreshToken(jwtSecurityToken);

        }

        private static bool CheckClaims(JwtSecurityToken jwt)
        {
            var expireDate = jwt.Claims.ToList().Find(m => m.Type.Equals("exp"));
            var expireDateTime = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt32(expireDate.Value));
            return expireDateTime >= DateTime.Now;
        }
    }
}