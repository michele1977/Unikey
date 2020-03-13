using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Ninject;
using NLog;
using UnikeyFactoryTest.IService;
using UnikeyFactoryTest.WebAPI.CustomAttributes;
using UnikeyFactoryTest.WebAPI.Models.DTO;

namespace UnikeyFactoryTest.WebAPI.Controllers
{
    [LoginAuthorize]
    [EnableCors("*", "*", "*")]
    public class ExTestController : ApiController
    {
        private readonly IKernel _kernel;
        private readonly ILogger _logger;
        private readonly IAdministratedTestService _service;

        public ExTestController(ILogger logger, IAdministratedTestService service, IKernel kernel)
        {
            _kernel = kernel;
            _service = service;
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

       
        public async Task<IHttpActionResult> GetAll()
        {
            try
            {
                var returned = await _service.GetAdministratedTests();
                return Ok(returned);
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
                var numberOfTests = await _service.CountExTests(filter);

                foreach(var test in tests)
                {
                    var testDto = new AdministratedTestDto(test);
                    returned.Add(testDto);
                }

                returned[0].NumberOfExTests = numberOfTests;

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
    }
}
