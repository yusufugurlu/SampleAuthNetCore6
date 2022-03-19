using FluentValidation;
using Sample.Models.UserViewModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Models.Validators.UserValidator
{
    public class AddUserValidator : AbstractValidator<AddUserViewModel>
    {
        public AddUserValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MinimumLength(2);
            RuleFor(x => x.SurName).MinimumLength(2);
            RuleFor(x => x.EMail).MinimumLength(2);
            RuleFor(x => x.Password).MinimumLength(2);
        }
    }
}
