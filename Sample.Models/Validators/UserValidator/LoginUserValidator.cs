using FluentValidation;
using Sample.Models.UserViewModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Models.Validators.UserValidator
{
    public class LoginUserValidator : AbstractValidator<LoginUserViewModel>
    {
        public LoginUserValidator()
        {
            RuleFor(x => x.EMail).MinimumLength(2);
            RuleFor(x => x.Password).MinimumLength(2);
        }
    }
}

