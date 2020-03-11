using System;
using System.Threading.Tasks;
using System.Web.Http;
using Ninject;
using NLog;
using UnikeyFactoryTest.IService;

namespace UnikeyFactoryTest.WebAPI.Controllers
{
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
