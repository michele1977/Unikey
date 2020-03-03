using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;
using Microsoft.AspNet.Identity;
using Ninject;
using NLog;
using UnikeyFactoryTest.Domain;
using UnikeyFactoryTest.IRepository;
using UnikeyFactoryTest.IService;
using UnikeyFactoryTest.Service;

namespace UnikeyFactoryTest.WebAPI.Controllers
{
    [EnableCors("*", "*", "*")]
    public class UserController : ApiController
    {
        private ILogger _logger;
        private readonly IKernel _kernel;
        private readonly UserManager<UserBusiness, int> _service;

        public UserController(IKernel kernel, ILogger logger)
        {
            _kernel = kernel;
            _logger = logger;
            _service = _kernel.Get<UserManager<UserBusiness, int>>();
        }

        //public UserController(UserManager<UserBusiness, int> service)
        //{
        //    _service = service;
        //}

        [HttpPost]
        public async Task<IHttpActionResult> Subscribe([FromBody] UserBusiness user)
        {
            try
            {
                var result = await _service.CreateAsync(user);

                if (result.Errors.Count() != 0)
                {
                    return BadRequest();
                }
            }
            catch (ArgumentException e)
            {
                _logger.Error(e, e.Message);
                return StatusCode(HttpStatusCode.Conflict);
            }
            catch (OverflowException e)
            {
                _logger.Error(e, e.Message);
                return StatusCode(HttpStatusCode.Conflict);
            }
            catch (Exception e)
            {
                _logger.Error(e, e.Message);
                return StatusCode(HttpStatusCode.Conflict);
            }

            _logger.Info($"{user.UserName} succesfully registered");

            return Ok();
        }
    }
}
