using AutoMapper;
using Sample.Business.Abstract;
using Sample.Business.GenericRepository;
using Sample.Common.Result;
using Sample.DataAccess.Entities;
using Sample.Models.ViewModel.BookTypeViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Business.Concrete
{
    public class BookTypeManager : IBoookTypeService
    {
        private readonly IUnitOfWorks _unitOfWorks;
        private readonly IMapper _mapper;
        private readonly IRedisService _redisService;
        private readonly IGenericRepository<BookType> _bookTypeRepo;

        public BookTypeManager(IUnitOfWorks unitOfWorks, IMapper mapper, IRedisService redisService)
        {
            _unitOfWorks = unitOfWorks;
            _mapper = mapper;
            _redisService = redisService;
            _bookTypeRepo = _unitOfWorks.GetGenericRepository<BookType>();

        }
        public ServiceResult Get(int id)
        {
            var bookType = new BookType();
            if (_redisService.IsSetKey("G_BookType"))
            {
                var bookTypes = _redisService.GetList<BookType>("G_BookType");
                bookType = bookTypes.Where(x => !x.IsDisabled && x.Id == id).FirstOrDefault();

            }
            else
            {
                var bookTypes = _bookTypeRepo.GetAll(x => !x.IsDisabled).ToList();
                _redisService.Set<BookType>("G_BookType", bookTypes);
                bookType = bookTypes.Where(x => !x.IsDisabled && x.Id == id).FirstOrDefault();
            }
            var bookTypeView = _mapper.Map<BookTypeViewModel>(bookType);
            return Result.Success("", bookTypeView);
        }

        public ServiceResult GetAll()
        {
            var bookTypes = new List<BookType>();
            if (_redisService.IsSetKey("G_BookType"))
            {
                bookTypes = _redisService.GetList<BookType>("G_BookType");

            }
            else
            {
                bookTypes = _bookTypeRepo.GetAll(x => !x.IsDisabled).ToList();
                _redisService.Set<BookType>("G_BookType", bookTypes);
            }
            var bookTypesView = _mapper.Map<List<BookTypeViewModel>>(bookTypes);
            return Result.Success("", bookTypesView);
        }
    }
}
