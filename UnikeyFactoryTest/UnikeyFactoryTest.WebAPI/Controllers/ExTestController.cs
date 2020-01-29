using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace UnikeyFactoryTest.WebAPI.Controllers
{
    public class ExTestController : ApiController
    {
        // GET: api/ExTest
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ExTest/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ExTest
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/ExTest/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ExTest/5
        public void Delete(int id)
        {
        }
    }
}
