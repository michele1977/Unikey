using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json.Linq;
using Ninject;
using NLog;
using UnikeyFactoryTest.Domain;
using UnikeyFactoryTest.Domain.Enums;
using UnikeyFactoryTest.IService;
using UnikeyFactoryTest.Service;
using UnikeyFactoryTest.WebAPI.Models.DTO;
using UnikeyFactoryTest.Service.Providers.MailProvider;
using UnikeyFactoryTest.WebAPI.CustomAttributes;
using UnikeyFactoryTest.WebAPI.Models;
using UnikeyFactoryTest.WebAPI_new.ResponseMessages;

namespace UnikeyFactoryTest.WebAPI.Controllers
{
    [EnableCors("*", "*", "*")]
    [LoginAuthorize]
    [RefreshAuthorize]
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
#if MOCK
            var fileContent = await Task.Run(() => File.ReadAllText(@"D:\mockTest.txt"));
            JObject json = JObject.Parse(fileContent);
            return Ok(json);
#endif

#if !MOCK
                try
                {
                    var returned = await _service.GetTestById(id);

                    return Ok(returned);
                }
                catch (ArgumentNullException e)
                {
                    _logger.Error(e, e.Message);
                    return InternalServerError();
                }
                catch (InvalidOperationException e)
                {
                    _logger.Error(e, e.Message);
                    return InternalServerError();
                }
                catch (Exception e)
                {
                    _logger.Fatal(e, e.Message);
                    return InternalServerError();
                }
#endif
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
            catch (ArgumentNullException e)
            {
                _logger.Error(e, e.Message);
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                _logger.Error(e, e.Message);
                return NotFound();
            }
        }


        public async Task<IHttpActionResult> GetAll(int pageNum, int pageSize, string filter)
        {
            try
            {
                var testBusinessList = await _service.GetAllFiltered(pageNum, pageSize, filter);
                var testDtoList = new List<TestDto>();

                foreach (var test in testBusinessList)
                {
                    var testDto = new TestDto(test, _service)
                    {
                        OpenedExTestNumber = await _service.GetExTestCountByState(test.Id, AdministratedTestState.Open),
                        NumberOfExTest = await _service.GetExTestCount(test.Id)
                    };
                    testDtoList.Add(testDto);
                }
                testDtoList[0].NumberOfTest = await _service.CountTests(filter);
               


                return Ok(testDtoList);
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
                test.UserId = 5;
                test.Date = DateTime.Now;

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
                test.UserId = 5;

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


        //[Route("api/Test/sendMail")]
        [HttpPost]
        public async Task<IHttpActionResult> SendMail(EmailModel emailModel)
        {
            TestBusiness test;

            try
            {
                test = await _service.GetTestByIdLight(emailModel.Id);
            }
            catch (ArgumentNullException e)
            {
                _logger.Error(e, e.Message);
                throw;
            }
            catch (InvalidOperationException e)
            {
                _logger.Error(e, e.Message);
                throw;
            }
            catch (DbUpdateConcurrencyException e)
            {
                _logger.Fatal(e, e.Message);
                throw;
            }
            catch (DbEntityValidationException e)
            {
                _logger.Fatal(e, e.Message);
                throw;
            }
            catch (DbUpdateException e)
            {
                _logger.Fatal(e, e.Message);
                throw;
            }
            catch (NotSupportedException e)
            {
                _logger.Error(e, e.Message);
                throw;
            }
            catch (Exception e)
            {
                _logger.Fatal(e, e.Message);
                throw;
            }

            bool result = false;
            TestDto sendTest = new TestDto(test, _service);
            var URL = sendTest.URL;
            MailProvider provider = new MailProvider();

            try
            {
                result = provider.SendMail(emailModel.email, emailModel.name, URL);
            }
            catch (HttpException e)
            {
                _logger.Error(e, e.Message);
            }
            catch (ArgumentNullException e)
            {
                _logger.Error(e, e.Message);

            }
            catch (ArgumentOutOfRangeException e)
            {
                _logger.Error(e, e.Message);
            }
            catch (ArgumentException e)
            {
                _logger.Error(e, e.Message);
            }
            catch (FormatException e)
            {
                _logger.Error(e, e.Message);
            }
            catch (ObjectDisposedException e)
            {
                _logger.Error(e, e.Message);
            }
            catch (InvalidOperationException e)
            {
                _logger.Error(e, e.Message);
            }
            catch (SmtpFailedRecipientException e)
            {
                _logger.Error(e, e.Message);
            }
            catch (SmtpException e)
            {
                _logger.Error(e, e.Message);
            }
            catch (Exception e)
            {
                _logger.Fatal(e, e.Message);
            }

            return Ok(result);
        }
    }
    
}
