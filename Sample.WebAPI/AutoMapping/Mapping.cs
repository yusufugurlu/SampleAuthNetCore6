using AutoMapper;
using Sample.DataAccess.Entities;
using Sample.Models.UserViewModel.ViewModel;
using Sample.Models.ViewModel.BookTypeViewModel;
using Sample.Models.ViewModel.BookViewModel;

namespace Sample.WebAPI.AutoMapping
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<AddUserViewModel, User>().ReverseMap();
            CreateMap<BookTypeViewModel, BookType>().ReverseMap();
            CreateMap<AddBookViewModel, Book>().ReverseMap();
            CreateMap<UpdateBookViewModel, Book>().ReverseMap();
            CreateMap<DisplayBookViewModel, Book>().ReverseMap().ForMember(dest => dest.BookType, opt => opt.MapFrom(src => (src.BookType.Name).ToString())); ;    
        }
    }
}
