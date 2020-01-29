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
        public async Task<JsonResult<AdministratedTestBusiness>> Get(int testId)
        {
            AdministratedTestService testService = new AdministratedTestService();
            var test = await testService.GetAdministratedTestById(testId);
            if (test == null) throw new Exception("Test non trovato");
            return Json(test);
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
        public async Task<IHttpActionResult> Put([FromBody]ExTestModel model)
        {
            AdministratedTestService service = new AdministratedTestService();
            var adtest = await service.GetAdministratedTestById(model.testId);
            var question = adtest.AdministratedQuestions.FirstOrDefault(x => x.Id == model.Question.Id);
            foreach (var answer in model.Question.AdministratedAnswers)
            {
                try
                {
                    question.AdministratedAnswers.FirstOrDefault(x => x.Id == answer.Id).isSelected = answer.isSelected;
                }
                catch (Exception e)
                {
                    throw new Exception("Question not found");
                }
            }
            await service.Update_Save_Question(question);

            return Ok();
        }

        // PATCH: api/ExTest
        public void Patch()
        {

        }

        // DELETE: api/ExTest/5
        public void Delete(int id)
        {
        }
    }
}
