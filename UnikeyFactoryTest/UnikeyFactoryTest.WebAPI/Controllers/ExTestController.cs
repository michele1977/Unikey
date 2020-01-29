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
using UnikeyFactoryTest.WebAPI.Models;

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
        public async Task<JsonResult<AdministratedTestBusiness>> Post([FromBody]ExTestModel model)
        {
            AdministratedTestService service = new AdministratedTestService();
            TestService testService = new TestService();
            var subject = model.name + " " + model.surname;
            var test = testService.GetTestByURL(model.guid);
            var administratedTest = service.AdministratedTest_Builder(test, subject);
            var savedTest = await service.Add(administratedTest);
            await service.ChangeAdministratedTestStateToStarted(savedTest.Id);
            return Json(savedTest);
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
