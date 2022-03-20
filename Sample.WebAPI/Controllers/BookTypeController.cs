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
    public class BookTypeController : ControllerBase
    {
        private readonly IBoookTypeService _bookTypeService;
        public BookTypeController(IBoookTypeService bookTypeService)
        {
            _bookTypeService = bookTypeService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            ResponseModel model = new ResponseModel();
            var result = _bookTypeService.GetAll();
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

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            ResponseModel model = new ResponseModel();
            var result = _bookTypeService.Get(id);
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
