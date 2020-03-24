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
using UnikeyFactoryTest.WebAPI.CustomExceptions;

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
                    throw new UnauthorizedException();
                if (!CheckClaims(jwtSecurityToken))
                    throw new RefreshUnauthorizedException();
            }
            catch (RefreshUnauthorizedException ex)
            {
                HandleUnauthorizedRequest(actionContext);
            }
            catch (UnauthorizedException ex)
            {
                base.HandleUnauthorizedRequest(actionContext);
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
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


        private static bool CheckSignature(JwtSecurityToken jwt)
        {
            if(!jwt.Header.Alg.Equals("HS256"))
                return false;
            
            var verifyString = CreateSignature(jwt);

            return jwt.RawSignature.Equals(verifyString);
        }

        private static string CreateSignature(JwtSecurityToken jwt)
        {
            var key = new SymmetricSecurityKey(Encoding.Default.GetBytes("MeFaSchifoLAgileyufntdbrsve"));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            return JwtTokenUtilities.CreateEncodedSignature(String.Concat(jwt.Header.Base64UrlEncode(), ".", jwt.Payload.Base64UrlEncode()), signingCredentials);
        }

        private static bool CheckClaims(JwtSecurityToken jwt)
        {
            var expireDate = jwt.Claims.ToList().Find(m => m.Type.Equals("exp"));
            var expireDateTime = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt32(expireDate.Value));
            return expireDateTime >= DateTime.Now;
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            if (actionContext == null)
                throw new Exception("Action Context is null");

            actionContext.Response = actionContext.ControllerContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Token is refreshing", new RefreshUnauthorizedException());
        }
    }
}