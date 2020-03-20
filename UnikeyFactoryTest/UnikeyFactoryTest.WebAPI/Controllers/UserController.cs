using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Ninject;
using NLog;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Results;
using UnikeyFactoryTest.Domain;
using UnikeyFactoryTest.WebAPI.Models.DTO;
using UnikeyFactoryTest.WebAPI.Tools;
using UnikeyFactoryTest.WebAPI_new.ResponseMessages;

namespace UnikeyFactoryTest.WebAPI.Controllers
{
    [EnableCors("*", "*", "*")]
    public class UserController : ApiController
    {
        private readonly IKernel _kernel;
        private readonly ILogger _logger;
        private readonly UserManager<UserBusiness, int> _service;
        private readonly SignInManager<UserBusiness, int> _signigni;

        public UserController(IKernel kernel, ILogger logger)
        {
            _kernel = kernel;
            _logger = logger;
            _service = _kernel.Get<UserManager<UserBusiness, int>>();
            _signigni = Request.GetOwinContext().Get<SignInManager<UserBusiness, int>>();
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Subscribe([FromBody] UserBusiness user)
        {
            try
            {
               var result = await _service.CreateAsync(user);

                if (result.Errors.Any())
                {
                    result.Errors.ToList().ForEach(e => ModelState.AddModelError(e,e));

                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                        ModelState);
                }
            }
            catch (ArgumentNullException e)
            {
                _logger.Error(e, e.Message);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                    ErrorMessages.InternalServerError);
            }
            catch (OverflowException e)
            {
                _logger.Error(e, e.Message);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                    ErrorMessages.InternalServerError);
            }
            catch (Exception e)
            {
                _logger.Error(e, e.Message);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                    ErrorMessages.InternalServerError);

            }

            return Request.CreateResponse(HttpStatusCode.OK, user.Id);
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Login(UserBusiness userBusiness)
        {
            try
            {
                var user = await _service.FindByNameAsync(userBusiness.UserName);

                if (user is null)
                    throw new ArgumentNullException();

                var status = await _signigni.PasswordSignInAsync(userBusiness.UserName, userBusiness.Password, false, false);
                
                if (status == SignInStatus.Failure)
                    throw new Exception("Invalid Password");
            }
            catch (ArgumentNullException e)
            {
                _logger.Error(e, e.Message);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                    ErrorMessages.InternalServerError);
            }
            catch (Exception e)
            {
                _logger.Error(e, e.Message);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                    ErrorMessages.InternalServerError);

            }

            var jwt = JwtFactory.GenerateToken(userBusiness);
            return Request.CreateResponse(HttpStatusCode.OK, jwt);
        }

        [HttpPost]
        public IHttpActionResult Refresh(JwtDto token)
        {
            var newJwt = JwtFactory.RefreshToken(token.Token);
            return Ok(newJwt);
        }

        [HttpGet]
        public JsonResult<string> TestAction()
        {
            return Json("Test Success");
        }
    }
}
