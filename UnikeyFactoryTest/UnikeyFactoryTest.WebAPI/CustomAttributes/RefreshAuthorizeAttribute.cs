using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Properties;
using UnikeyFactoryTest.WebAPI.Tools;

namespace UnikeyFactoryTest.WebAPI.CustomAttributes
{
    public class RefreshAuthorizeAttribute : AuthorizeAttribute
    {
        //public override void OnAuthorization(HttpActionContext actionContext)
        //{
        //    var jwt = actionContext.Request.Headers.Authorization.Parameter;
        //    var jwtHandler = new JwtSecurityTokenHandler();
        //    var jwtSecurityToken = jwtHandler.ReadJwtToken(jwt);

        //    try
        //    {
                
        //    }
        //    catch (Exception e)
        //    {
        //        var newJwt = JwtFactory.RefreshToken(jwtSecurityToken);
        //        actionContext.Response = actionContext.ControllerContext.Request.CreateErrorResponse(HttpStatusCode.,);
        //    }
        //}
    }
}