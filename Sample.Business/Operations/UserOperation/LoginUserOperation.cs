using FluentValidation.Results;
using Sample.Business.Abstract;
using Sample.Common.Helper;
using Sample.Common.Response;
using Sample.Models.UserViewModel.ViewModel;
using Sample.Models.Validators.UserValidator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Business.Operations.UserOperation
{
    public class LoginUserOperation
    {
        public LoginUserViewModel LoginUserViewModel { get; set; }
        public readonly IAccessService _accessService;
        public LoginUserOperation(IAccessService accessService)
        {
            _accessService = accessService;
        }

        public ResponseModel Handle()
        {
            ResponseModel model = new ResponseModel();
            LoginUserValidator validator = new LoginUserValidator();
            ValidationResult results = validator.Validate(LoginUserViewModel);

            if (!results.IsValid)
            {
                var errors = results.Errors.Select(x => x.ErrorMessage).ToList();
                var errorListLiTags = ErrorList.ErrorListHtmlTag(errors);
                model.Message = errorListLiTags;
                return model;
            }

            var result = _accessService.Login(LoginUserViewModel);
            if (result.IsSuccess)
            {
                model.Data = result.Data;
                model.Status=result.Status();
                return model;
            }
            model.Message = result.Message;
            model.Status = 401;
            return model;
        }
    }
}
