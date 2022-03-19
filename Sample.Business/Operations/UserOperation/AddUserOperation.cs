using FluentValidation.Results;
using Sample.Business.Abstract;
using Sample.Common.Helper;
using Sample.Common.Response;
using Sample.Common.Result;
using Sample.Models.UserViewModel.ViewModel;
using Sample.Models.Validators.UserValidator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Business.Operations.UserOperation
{
    public class AddUserOperation
    {
        public AddUserViewModel AddUserViewModel { get; set; }
        public readonly IAccessService _accessService;
        public AddUserOperation(IAccessService accessService)
        {
            _accessService = accessService;
        }

        public ResponseModel Handle()
        {
            ResponseModel model = new ResponseModel();
            AddUserValidator validator = new AddUserValidator();
            ValidationResult results = validator.Validate(AddUserViewModel);

            if (!results.IsValid)
            {
                var errors = results.Errors.Select(x => x.ErrorMessage).ToList();
                var errorListLiTags = ErrorList.ErrorListHtmlTag(errors);
                model.Message = errorListLiTags;
                return model;
            }

            var result = _accessService.AddUser(AddUserViewModel);
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
