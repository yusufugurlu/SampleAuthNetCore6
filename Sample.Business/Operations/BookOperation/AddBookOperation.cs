using FluentValidation.Results;
using Sample.Business.Abstract;
using Sample.Common.Helper;
using Sample.Common.Response;
using Sample.Models.Validators.BookValidator;
using Sample.Models.ViewModel.BookViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Business.Operations.BookOperation
{
    public class AddBookOperation
    {
        public AddBookViewModel AddBookViewModel { get; set; }
        public readonly IBookService _bookService;
        public AddBookOperation(IBookService bookService)
        {
            _bookService = bookService;
        }

        public ResponseModel Handle()
        {
            ResponseModel model = new ResponseModel();
            AddBookValidator validator = new AddBookValidator();
            ValidationResult results = validator.Validate(AddBookViewModel);

            if (!results.IsValid)
            {
                var errors = results.Errors.Select(x => x.ErrorMessage).ToList();
                var errorListLiTags = ErrorList.ErrorListHtmlTag(errors);
                model.Message = errorListLiTags;
                return model;
            }

            var result = _bookService.AddBook(AddBookViewModel);
            if (result.IsSuccess)
            {
                model.Data = result.Data;
            }
            model.Message = result.Message;
            model.Status = result.Status();
            return model;
        }
    }
}
