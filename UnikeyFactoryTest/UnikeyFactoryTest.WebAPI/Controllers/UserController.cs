using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Microsoft.AspNet.Identity;
using Ninject;
using NLog;
using UnikeyFactoryTest.Domain;
using UnikeyFactoryTest.WebAPI.ResponseMessages;

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
        public async Task<HttpResponseMessage> Subscribe([FromBody] UserDto user)
        {
            try
            {
                var userBusiness = new UserBusiness()
                {
                    UserName = user.UserName,
                    Password = user.Password
                };

                var result = await _service.CreateAsync(userBusiness);

                if (result.Errors.Count() != 0)
                {
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

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
