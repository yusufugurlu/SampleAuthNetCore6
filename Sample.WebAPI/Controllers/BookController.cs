using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sample.Business.Abstract;
using Sample.Business.Operations.BookOperation;
using Sample.Common.Response;
using Sample.Models.ViewModel.BookViewModel;
using System.Net;

namespace Sample.WebAPI.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    [Authorize]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            ResponseModel model = new ResponseModel();
            var result = _bookService.GetAll();
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
            var result = _bookService.GetBook(id);
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

        [HttpPost]
        public IActionResult AddBook(AddBookViewModel model)
        {
            AddBookOperation addBookOperation = new AddBookOperation(_bookService);
            addBookOperation.AddBookViewModel = model;
            var result = addBookOperation.Handle();
            if (result.Status == (int)HttpStatusCode.OK)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost]
        public IActionResult UpdateBook(UpdateBookViewModel model)
        {
            UpdateBookOperation addBookOperation = new UpdateBookOperation(_bookService);
            addBookOperation.UpdateBookViewModel = model;
            var result = addBookOperation.Handle();
            if (result.Status == (int)HttpStatusCode.OK)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost()]
        public IActionResult DeleteBook([FromQuery]int id)
        {
            ResponseModel model = new ResponseModel();
            var result = _bookService.DeleteBook(id);
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
