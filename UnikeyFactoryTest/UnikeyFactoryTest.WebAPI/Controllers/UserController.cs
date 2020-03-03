using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using UnikeyFactoryTest.Domain;

namespace UnikeyFactoryTest.WebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UserController : ApiController
    {
        private readonly UserManager<UserBusiness, int> _service;

        public UserController()
        {
            
        }

        public UserController(UserManager<UserBusiness, int> value)
        {
            _service = value;
        }

        [HttpPost]

        public async Task<IHttpActionResult> Subscribe([FromBody]UserBusiness user)
        {
            var result = await _service.CreateAsync(user);

            if (result.Errors.Count() != 0)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
