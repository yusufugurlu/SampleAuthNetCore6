using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sample.Business.Abstract;

namespace Sample.WebAPI.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    [Authorize]
    public class ManagmentController : ControllerBase
    {
        private readonly IManagmentService _managmentService;
        public ManagmentController(IManagmentService managmentService)
        {
            _managmentService = managmentService;
        }
        [HttpGet]
        public IActionResult GetOnlineUser( )
        {
           var result= _managmentService.GetOnlineUser();
            return Ok(result);
        }

        [HttpGet]
        public IActionResult GetNewRegisterUserOneDay()
        {
            var result = _managmentService.GetNewRegisterUserOneDay();
            return Ok(result);
        }
        [HttpGet]
        public IActionResult GetSendActivitionButNotUsedOneDay()
        {
            var result = _managmentService.GetSendActivitionButNotUsedOneDay();
            return Ok(result);
        }

        [HttpGet]
        public IActionResult GetLoginRequestTimeStampAverrage()
        {
            var result = _managmentService.GetLoginRequestTimeStampAverrage();
            return Ok(result);
        }
    }
}
