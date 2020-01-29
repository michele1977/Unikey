using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UnikeyFactoryTest.Domain;
using UnikeyFactoryTest.Service;

namespace UnikeyFactoryTest_WebApi.Controllers
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
        public async JsonResult Post(string GUID, string Name, string Surname)
        {
            AdministratedTestService service = new AdministratedTestService();
            TestService testService = new TestService();
            var subject = Name + " " + Surname;
            var test = testService.GetTestByURL(GUID);
            var AdTest = service.AdministratedTest_Builder(test, subject);
            await service.Add(AdTest);
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
