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
using UnikeyFactoryTest.IService;
using UnikeyFactoryTest.WebAPI.CustomAttributes;
using UnikeyFactoryTest.WebAPI.Models.DTO;
using UnikeyFactoryTest.WebAPI.Models.DTO;

namespace UnikeyFactoryTest.WebAPI.Controllers
{
    [LoginAuthorize]
    [RefreshAuthorize]
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
    }
}
