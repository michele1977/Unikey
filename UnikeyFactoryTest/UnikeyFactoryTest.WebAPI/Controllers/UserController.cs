using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Ninject;
using NLog;
using UnikeyFactoryTest.Domain;

namespace UnikeyFactoryTest.WebAPI.Controllers
{
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
        public async Task<IHttpActionResult> Subscribe([FromBody] UserBusiness user)
        {
            try
            {
                var result = await _service.CreateAsync(user);

                if (result.Errors.Count() != 0)
                    return BadRequest();
            }
            catch (ArgumentNullException e)
            {
                _logger.Error(e, e.Message);
                return Conflict();
            }
            catch (OverflowException e)
            {
                _logger.Error(e, e.Message);
                return Conflict();
            }
            catch (Exception e)
            {
                _logger.Error(e, e.Message);
                return Conflict();
            }

            return Ok();
        }
    }
}
