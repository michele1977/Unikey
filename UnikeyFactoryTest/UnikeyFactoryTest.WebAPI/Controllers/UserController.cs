using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Results;
using Microsoft.AspNet.Identity;
using Ninject;
using NLog;
using UnikeyFactoryTest.Domain;
using UnikeyFactoryTest.WebAPI.CustomAttributes;
using UnikeyFactoryTest.WebAPI.ResponseMessages;
using UnikeyFactoryTest.WebAPI.Tools;

namespace UnikeyFactoryTest.WebAPI.Controllers
{
    [EnableCors("*", "*", "*")]
    public class UserController : ApiController
    {
        private readonly IKernel _kernel;
        private readonly ILogger _logger;
        private readonly UserManager<UserBusiness, int> _service;

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

                if (result.Errors.Count() != 0)
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

        [HttpPost]
        public async Task<HttpResponseMessage> Login(UserBusiness userBusiness)
        {
            try
            {
                var user = await _service.FindByNameAsync(userBusiness.UserName);

                if (user is null)
                    throw new ArgumentNullException();

                if(!await _service.CheckPasswordAsync(user, userBusiness.Password))
                    throw new Exception("Invalid Password");
            }
            catch (ArgumentNullException e)
            {
                //_logger.Error(e, e.Message);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                    ErrorMessages.InternalServerError);
            }
            catch (Exception e)
            {
                //_logger.Error(e, e.Message);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                    ErrorMessages.InternalServerError);

            }

            var jwt = JwtFactory.GenerateToken(userBusiness);

            return Request.CreateResponse(HttpStatusCode.OK, jwt);
        }

        [LoginAuthorize]
        [HttpPost]
        public JsonResult<string> TestAction()
        {
            return Json("Test Success");
        }
    }
}
