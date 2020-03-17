using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Properties;
using System.Web.Http.Results;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace UnikeyFactoryTest.WebAPI.CustomAttributes
{
    public class LoginAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var jwt = actionContext.Request.Headers.Authorization.Parameter;
            var jwtHandler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = jwtHandler.ReadJwtToken(jwt);
            
            try
            {
                if (!CheckJwtStructure(jwt) || !CheckSignature(jwtSecurityToken))
                    throw new Exception();
            }
            catch
            {
                base.HandleUnauthorizedRequest(actionContext);
            }

            try
            {
                if (!CheckClaims(jwtSecurityToken))
                    throw new Exception();
            }
            catch
            {
                actionContext.Response = actionContext.ControllerContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Token has expired");
            }
        }

        public static bool CheckJwtStructure(string jwt)
        {
            const string regexJwt = @"^[A-Za-z0-9-_=]+\.[A-Za-z0-9-_=]+\.[A-Za-z0-9-_.+/=]*$";
            const string regexJson = @"\{.*\:.*\:.*\}";

            if(!Regex.IsMatch(jwt, regexJwt))
                return false;

            var jwtComponents = jwt.Split('.').Take(2);


            if(!jwtComponents.All(jwtComponent => Regex.IsMatch(Base64UrlEncoder.Decode(jwtComponent), regexJson)))
                return false;

            return true;
        }

        public static bool CheckClaims(JwtSecurityToken jwt)
        {
            var expireDate = jwt.Claims.ToList().Find(m => m.Type.Equals("exp"));
            var expireDateTime = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt32(expireDate.Value));
            return expireDateTime >= DateTime.Now;
        }

        public static bool CheckSignature(JwtSecurityToken jwt)
        {
            if(!jwt.Header.Alg.Equals("HS256"))
                return false;
            
            var verifyString = CreateSignature(jwt);

            return jwt.RawSignature.Equals(verifyString);
        }

        public static string CreateSignature(JwtSecurityToken jwt)
        {
            var key = new SymmetricSecurityKey(Encoding.Default.GetBytes("MeFaSchifoLAgile"));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            return JwtTokenUtilities.CreateEncodedSignature(string.Concat(jwt.Header.Base64UrlEncode(), ".", jwt.Payload.Base64UrlEncode()), signingCredentials);
        }
    }
}