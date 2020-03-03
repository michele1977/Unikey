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
using UnikeyFactoryTest.Domain;
using UnikeyFactoryTest.IRepository;
using UnikeyFactoryTest.IService;
using UnikeyFactoryTest.Service;

namespace UnikeyFactoryTest.WebAPI.Controllers
{
    [EnableCors("*", "*", "*")]
    public class UserController : ApiController
    {
        private IKernel kernel;
        private UserManager<UserBusiness, int> _service;

        public UserController()
        {
            
        }

        public UserController(IKernel k)
        {
            kernel = k;
            _service = k.Get<UserManager<UserBusiness, int>>();
        }

        [HttpPost]
        public async Task<IHttpActionResult> Subscribe([FromBody] UserBusiness user)
        {
            var result = await _service.CreateAsync(user);

            if (result.Errors.Count() != 0)
            {
                return Unauthorized();
            }

            return Ok();
        }
    }
}
