using System;
using System.Web.Http;
using System.Web.Http.Cors;
using UnikeyFactoryTest.Domain;

namespace UnikeyFactoryTest.WebAPI.Controllers
{
    [EnableCors("*", "*", "*")]
    public class FakeUserApiController : ApiController
    {

        [HttpPost]
        public IHttpActionResult Login([FromBody]UserBusiness user)
        {
            try
            {
                if (user.UserName == "Federicchione" && user.Password == "Unikey1!")
                {
                    return Ok(user);
                }
                throw new Exception();
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IHttpActionResult Subscribe([FromBody]UserBusiness user)
        {
            return Ok(user);
        }
    }
}