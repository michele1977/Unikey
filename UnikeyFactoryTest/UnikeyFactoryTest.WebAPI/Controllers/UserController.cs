using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Results;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Ninject;
using Ninject.Parameters;
using NLog;
using UnikeyFactoryTest.Domain;
using UnikeyFactoryTest.WebAPI.CustomAttributes;
using UnikeyFactoryTest.WebAPI.Tools;
using UnikeyFactoryTest.WebAPI_new.ResponseMessages;

namespace UnikeyFactoryTest.WebAPI.Controllers
{
    [EnableCors("*", "*", "*")]
    public class UserController : ApiController
    {
        private readonly IKernel _kernel;
        private readonly ILogger _logger;
        private readonly UserManager<UserBusiness, int> _service;
        private readonly SignInManager<UserBusiness, int> _signigni;

        public UserController(IKernel kernel, ILogger logger)
        {
            _kernel = kernel;
            _logger = logger;
            _service = _kernel.Get<UserManager<UserBusiness, int>>();
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Subscribe([FromBody] UserBusiness user)
        {
            try
            {
               var result = await _service.CreateAsync(user);

                if (result.Errors.Any())
                {
                    result.Errors.ToList().ForEach(e => ModelState.AddModelError(e,e));

                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                        ModelState);
                }
            }
            catch (ArgumentNullException e)
            {
                _logger.Error(e, e.Message);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                    ErrorMessages.InternalServerError);
            }
            catch (OverflowException e)
            {
                _logger.Error(e, e.Message);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                    ErrorMessages.InternalServerError);
            }
            catch (Exception e)
            {
                _logger.Error(e, e.Message);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                    ErrorMessages.InternalServerError);

            }

            return Request.CreateResponse(HttpStatusCode.OK, user.Id);
        }

        [LoginAuthorize]
        [HttpPost]
        public async Task<HttpResponseMessage> Login(UserBusiness userBusiness)
        {
            try
            {
                var user = await _service.FindByNameAsync(userBusiness.UserName);

                if (user is null)
                    throw new ArgumentNullException();

                if (!await _service.CheckPasswordAsync(user, userBusiness.Password))
                    throw new Exception("Invalid Password");

                //await _signigni.SignInAsync(user, false, false);
            }
            catch (ArgumentNullException e)
            {
                _logger.Error(e, e.Message);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                    ErrorMessages.InternalServerError);
            }
            catch (Exception e)
            {
                _logger.Error(e, e.Message);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                    ErrorMessages.InternalServerError);

            }

            var jwt = JwtFactory.GenerateToken(userBusiness);
            return Request.CreateResponse(HttpStatusCode.OK, jwt);
        }

        public class JwtObj
        {
            public string token { get; set; }
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Refresh(JwtObj token)
        {
            //var newJwt = JwtFactory.RefreshToken("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjAiLCJ1c2VyTmFtZSI6IkxvbGxvbG9uZSIsIm5iZiI6MTU4NDYyNTEzMiwiZXhwIjoxNTg0NjI1MTkyLCJpYXQiOjE1ODQ2MjUxMzIsImlzcyI6Imlzc3VlciIsImF1ZCI6IkF1ZGllbmNlIn0.WG81BJlGgMo6mFALtwNUfWPKKk3OZxl5moJB-s1taAg");
            var newJwt = JwtFactory.RefreshToken(token.token);
            return Request.CreateResponse(HttpStatusCode.OK, newJwt);
        }

        [HttpGet]
        public JsonResult<string> TestAction()
        {
            return Json("Test Success");
        }
    }
}
