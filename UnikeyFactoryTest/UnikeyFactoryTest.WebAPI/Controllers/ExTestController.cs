using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Ninject;
using Ninject.Infrastructure.Language;
using NLog;
using UnikeyFactoryTest.Domain.Enums;
using UnikeyFactoryTest.IService;
using UnikeyFactoryTest.WebAPI.Models.DTO;

namespace UnikeyFactoryTest.WebAPI.Controllers
{
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

        public async Task<IHttpActionResult> GetByTestId(int id)
        {
            try
            {
                var returned = await _service.GetAdministratedTestsByTestId(id);
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
