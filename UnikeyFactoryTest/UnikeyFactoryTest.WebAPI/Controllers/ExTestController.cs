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
<<<<<<< HEAD
=======
using UnikeyFactoryTest.WebAPI.Models;
>>>>>>> c637e23ad3b20b33f122feb98cb85bee9b967c85

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

<<<<<<< HEAD
            // POST: api/ExTest
        public void Post([FromBody]string value)
=======
        // POST: api/ExTest
        public async Task<JsonResult<AdministratedTestBusiness>> Post([FromBody]ExTestModel model)
>>>>>>> c637e23ad3b20b33f122feb98cb85bee9b967c85
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
