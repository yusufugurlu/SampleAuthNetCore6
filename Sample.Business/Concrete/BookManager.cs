using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sample.Business.Abstract;
using Sample.Business.GenericRepository;
using Sample.Common.Result;
using Sample.DataAccess.Entities;
using Sample.Models.ViewModel.BookViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Business.Concrete
{
    public class BookManager : IBookService
    {
        private readonly IUnitOfWorks _unitOfWorks;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Book> _bookRepo;
        public BookManager(IUnitOfWorks unitOfWorks, IMapper mapper)
        {
            _unitOfWorks = unitOfWorks;
            _mapper = mapper;
            _bookRepo = _unitOfWorks.GetGenericRepository<Book>();
        }
        public ServiceResult AddBook(AddBookViewModel model)
        {
            var book= _mapper.Map<Book>(model);
            _bookRepo.Add(book);
            return _unitOfWorks.SaveChanges();
        }

        public ServiceResult DeleteBook(int id)
        {
           var book = _bookRepo.Get(id);
            if (book != null)
            {
                _bookRepo.Delete(book);
                return _unitOfWorks.SaveChanges();
            }

            return Result.Fail("Kitap bulunamadı");
        }

        public ServiceResult GetAll()
        {
            var books=_bookRepo.GetAll(x=>!x.IsDisabled).Include(x=>x.BookType).ToList();
            var booksView = _mapper.Map<List<DisplayBookViewModel>>(books);
            return Result.Success("",booksView);
        }

        public ServiceResult GetBook(int id)
        {
            var books = _bookRepo.GetAll(x => !x.IsDisabled && x.Id==id).Include(x => x.BookType).FirstOrDefault();
            var booksView = _mapper.Map<DisplayBookViewModel>(books);
            return Result.Success("", booksView);
        }

        public ServiceResult UpdateBook(UpdateBookViewModel model)
        {
            var book = _mapper.Map<Book>(model);
            _bookRepo.Update(book);
            return _unitOfWorks.SaveChanges();
        }
    }
}
