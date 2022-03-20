using FluentValidation;
using Sample.Models.ViewModel.BookViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Models.Validators.BookValidator
{
    public class AddBookValidator : AbstractValidator<AddBookViewModel>
    {
        public AddBookValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.ISBN).NotEmpty();
            RuleFor(x => x.Amount).GreaterThan(0);
            RuleFor(x => x.BookTypeId).GreaterThan(0);
        }
    }
}
