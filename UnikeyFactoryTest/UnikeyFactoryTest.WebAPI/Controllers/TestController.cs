using System;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Ninject;
using NLog;
using UnikeyFactoryTest.Domain;
using UnikeyFactoryTest.IService;
using UnikeyFactoryTest.Service;
using UnikeyFactoryTest.WebAPI.ResponseMessages;

namespace UnikeyFactoryTest.WebAPI.Controllers
{
    [EnableCors("*", "*", "*")]
    public class TestController : ApiController
    {
        private readonly IKernel _kernel;
        private readonly ILogger _logger;
        private readonly ITestService _service;

        public TestController(IKernel kernel, ILogger logger)
        {
            _kernel = kernel;
            _logger = logger;
            _service = _kernel.Get<TestService>();
        }

        public async Task<IHttpActionResult> Get(int id)
        {
            try
            {
                var returned = await _service.GetTestById(id);

                return Ok(returned);
            }
            catch (Exception e)
            {
                _logger.Error(e, e.Message);
                return NotFound();
            }
        }

        public async Task<IHttpActionResult> GetByUrl(string url)
        {
            try
            {
                var returned = await _service.GetTestByURL(url);

                return Ok(returned);
            }
            catch (Exception e)
            {
                _logger.Error(e, e.Message);
                return NotFound();
            }
        }

        public async Task<IHttpActionResult> GetByFilter(string filter)
        {
            try
            {
                var returned = await _service.GetTestsByFilter(filter);

                return Ok(returned);
            }
            catch (Exception e)
            {
                _logger.Error(e, e.Message);
                return NotFound();
            }
        }


        public async Task<IHttpActionResult> GetAll()
        {
            try
            {
                var returned = await _service.GetTests();
                return Ok(returned);
            }
            catch (Exception e)
            {
                _logger.Fatal(e, e.Message);
                return InternalServerError();
            }
        }

        public async Task<IHttpActionResult> Delete(int id)
        {
            try
            {
                await _service.DeleteTest(id);
                return Ok(id);
            }
            catch (Exception e)
            {
                _logger.Error(e, e.Message);
                return NotFound();
            }
        }

        [HttpPost]
        public HttpResponseMessage Create(TestBusiness test)
        {
            try
            {
                _service.AddNewTest(test);
                return Request.CreateResponse(HttpStatusCode.OK, test.Id);
            }
            catch (ArgumentNullException e)
            {
                _logger.Error(e, e.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ErrorMessages.InternalServerError);
            }
            catch (NotSupportedException e)
            {
                _logger.Error(e, e.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ErrorMessages.InternalServerError);
            }
            catch (ObjectDisposedException e)
            {
                _logger.Error(e, e.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ErrorMessages.InternalServerError);
            }
            catch (InvalidOperationException e)
            {
                _logger.Error(e, e.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ErrorMessages.InternalServerError);
            }
            catch (DbUpdateConcurrencyException e)
            {
                _logger.Fatal(e, e.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ErrorMessages.InternalServerError);
            }
            catch (DbUpdateException e)
            {
                _logger.Fatal(e, e.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ErrorMessages.InternalServerError);
            }
            catch (DbEntityValidationException e)
            {
                _logger.Fatal(e, e.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ErrorMessages.InternalServerError);
            }
            catch (Exception e)
            {
                _logger.Fatal(e, e.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ErrorMessages.InternalServerError);
            }
        }

        [HttpPatch]
        public async Task<HttpResponseMessage> Update(TestBusiness test)
        {
            try
            {
                await _service.UpdateTest(test);
                return Request.CreateResponse(HttpStatusCode.OK, test.Id);
            }
            catch (NullReferenceException e)
            {
                _logger.Error(e, e.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ErrorMessages.InternalServerError);
            }
            catch (ArgumentNullException e)
            {
                _logger.Error(e, e.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ErrorMessages.InternalServerError);
            }
            catch (NotSupportedException e)
            {
                _logger.Error(e, e.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ErrorMessages.InternalServerError);
            }
            catch (ObjectDisposedException e)
            {
                _logger.Error(e, e.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ErrorMessages.InternalServerError);
            }
            catch (InvalidOperationException e)
            {
                _logger.Error(e, e.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ErrorMessages.InternalServerError);
            }
            catch (DbUpdateConcurrencyException e)
            {
                _logger.Fatal(e, e.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ErrorMessages.InternalServerError);
            }
            catch (DbUpdateException e)
            {
                _logger.Fatal(e, e.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ErrorMessages.InternalServerError);
            }
            catch (DbEntityValidationException e)
            {
                _logger.Fatal(e, e.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ErrorMessages.InternalServerError);
            }
            catch (Exception e)
            {
                _logger.Fatal(e, e.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ErrorMessages.InternalServerError);
            }
        }
    }
}
