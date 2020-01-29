using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using UnikeyFactoryTest.Domain;
using UnikeyFactoryTest.Service;

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
        public async Task<JsonResult<AdministratedTestBusiness>> Get(int testId)
        { 
            AdministratedTestService testService = new AdministratedTestService();
            var test = await testService.GetAdministratedTestById(testId);
            if (test == null) throw new Exception("Test non trovato");
            return Json(test);
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
