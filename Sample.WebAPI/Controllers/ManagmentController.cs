using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sample.Business.Abstract;
using Sample.Common.Response;

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
        public IActionResult GetOnlineUser()
        {
            ResponseModel model = new ResponseModel();
            var result = _managmentService.GetOnlineUser();
            if (result.IsSuccess)
            {
                model.Data = result.Data;
                model.Message = result.Message;
                model.Status = result.Status();
                return Ok(model);
            }
            model.Message = result.Message;
            model.Status = result.Status();
            return BadRequest(model);
        }

        [HttpGet]
        public IActionResult GetNewRegisterUserOneDay()
        {
           
            ResponseModel model = new ResponseModel();
            var result = _managmentService.GetNewRegisterUserOneDay();
            if (result.IsSuccess)
            {
                model.Data = result.Data;
                model.Message = result.Message;
                model.Status = result.Status();
                return Ok(model);
            }
            model.Message = result.Message;
            model.Status = result.Status();
            return BadRequest(model);
        }
        [HttpGet]
        public IActionResult GetSendActivitionButNotUsedOneDay()
        {
        
            ResponseModel model = new ResponseModel();
            var result = _managmentService.GetSendActivitionButNotUsedOneDay();
            if (result.IsSuccess)
            {
                model.Data = result.Data;
                model.Message = result.Message;
                model.Status = result.Status();
                return Ok(model);
            }
            model.Message = result.Message;
            model.Status = result.Status();
            return BadRequest(model);
        }

        [HttpGet]
        public IActionResult GetLoginRequestTimeStampAverrage()
        {         
            ResponseModel model = new ResponseModel();
            var result = _managmentService.GetLoginRequestTimeStampAverrage();
            if (result.IsSuccess)
            {
                model.Data = result.Data;
                model.Message = result.Message;
                model.Status = result.Status();
                return Ok(model);
            }
            model.Message = result.Message;
            model.Status = result.Status();
            return BadRequest(model);
        }
    }
}
