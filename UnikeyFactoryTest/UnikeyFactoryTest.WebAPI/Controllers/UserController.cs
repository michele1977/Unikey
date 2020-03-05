using System;
using System.Collections.Generic;
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
        public async Task<HttpResponseMessage> Subscribe([FromBody] UserBusiness user)
        {
            try
            {
                var result = await _service.CreateAsync(user);

                if (result.Errors.Count() != 0)
                {
                    var validationMessages = result.Errors.ToList();
                    return Request.CreateResponse(HttpStatusCode.BadRequest, validationMessages);
                }
            }
            catch (ArgumentNullException e)
            {
                _logger.Error(e, e.Message);
                return Request.CreateResponse(HttpStatusCode.Conflict, ErrorMessages.Unexpected_error);
            }
            catch (OverflowException e)
            {
                _logger.Error(e, e.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ErrorMessages.Unexpected_error);
            }
            catch (Exception e)
            {
                _logger.Error(e, e.Message);
                return Request.CreateResponse(HttpStatusCode.Conflict, ErrorMessages.Unexpected_error);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
