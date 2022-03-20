using Sample.Common.Result;
using Sample.Models.ViewModel.BookViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Business.Abstract
{
    public interface IBookService
    {
        ServiceResult AddBook(AddBookViewModel model);
        ServiceResult UpdateBook(UpdateBookViewModel model);
        ServiceResult GetBook(int id);
        ServiceResult DeleteBook(int id);
        ServiceResult GetAll();
    }
}
