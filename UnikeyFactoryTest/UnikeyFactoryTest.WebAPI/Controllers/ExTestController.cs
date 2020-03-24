using Ninject;
using NLog;
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Cors;
using Ninject;
using NLog;
using UnikeyFactoryTest.Domain;
using UnikeyFactoryTest.IService;
using UnikeyFactoryTest.WebAPI.CustomAttributes;
using UnikeyFactoryTest.WebAPI.Models.DTO;
using UnikeyFactoryTest.WebAPI.Models.DTO;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;

namespace UnikeyFactoryTest.WebAPI.Controllers
{
    [LoginAuthorize]
    [EnableCors("*", "*", "*")]
    public class ExTestController : ApiController
    {
        private readonly IKernel _kernel;
        private readonly ILogger _logger;
        private readonly IAdministratedTestService _service;
        private readonly ITestService _testService;

        public ExTestController(ILogger logger, IAdministratedTestService service, ITestService testService, IKernel kernel)
        {
            _kernel = kernel;
            _service = service;
            _testService = testService;
            _logger = logger;
        }

        public async Task<IHttpActionResult> Get(int id)
        {
            try
            {
                var returned = await _service.GetAdministratedTestById(id);
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
        }

        public async Task<IHttpActionResult> GetAll(int pageNum, int pageSize, string filter)
        {
            try
            {
                var testBusinessList = await _service.GetAllFiltered(pageNum, pageSize, filter);
                var exTestDtoList = testBusinessList.Select(t => new ExTestDto(_service, t)).ToList();
                exTestDtoList[0].TotalNumberOfExTests = await _service.CountExTests(filter);

                return Ok(exTestDtoList);
            }
            catch (ArgumentNullException e)
            {
                _logger.Error(e, e.Message);
                return InternalServerError();
            }
            catch (Exception e)
            {
                _logger.Fatal(e, e.Message);
                return InternalServerError();
            }
        }

        public async Task<IHttpActionResult> GetByTestId(int pageNum, int pageSize, string filter, int id)
        {
            try
            {
                var tests = await _service.GetAdministratedTestsByTestId(pageNum, pageSize, filter, id);
                var returned = new List<AdministratedTestDto>();
                var numberOfTests = await _service.CountExTests(filter, id);

                if (numberOfTests > 0)
                {
                    foreach (var test in tests)
                    {
                        var testDto = new AdministratedTestDto(test);
                        returned.Add(testDto);
                    }

                    returned[0].NumberOfExTests = numberOfTests;
                }

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
        }

        public async Task<IHttpActionResult> GetExTestByTestUrl(string guid, string subject)
        {
            try
            {
                var test = await _testService.GetTestByURL(guid);
                var exTest = _service.AdministratedTest_Builder(test, subject);

                return Ok(exTest);
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
            catch (NullReferenceException e)
            {
                _logger.Error(e, e.Message);
                return InternalServerError();
            }
            catch (Exception e)
            {
                _logger.Fatal(e, e.Message);
                return InternalServerError();
            }
        }

        [HttpPost]
        public IHttpActionResult Create(AdministratedTestBusiness exTest)
        {
            try
            {
                var exTestBusiness = _service.Add(exTest);
                return Ok(exTestBusiness);
            }
            catch (ArgumentNullException e)
            {
                _logger.Error(e, e.Message);
                return InternalServerError();
            }
            catch (NotSupportedException e)
            {
                _logger.Error(e, e.Message);
                return InternalServerError();
            }
            catch (ObjectDisposedException e)
            {
                _logger.Error(e, e.Message);
                return InternalServerError();
            }
            catch (InvalidOperationException e)
            {
                _logger.Error(e, e.Message);
                return InternalServerError();
            }
            catch (DbUpdateConcurrencyException e)
            {
                _logger.Fatal(e, e.Message);
                return InternalServerError();
            }
            catch (DbUpdateException e)
            {
                _logger.Fatal(e, e.Message);
                return InternalServerError();
            }
            catch (DbEntityValidationException e)
            {
                _logger.Fatal(e, e.Message);
                return InternalServerError();
            }
            catch (Exception e)
            {
                _logger.Fatal(e, e.Message);
                return InternalServerError();
            }
        }
    }
}
