using AutoMapper;
using Sample.DataAccess.Entities;
using Sample.Models.UserViewModel.ViewModel;

namespace Sample.WebAPI.AutoMapping
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<AddUserViewModel, User>().ReverseMap();
        }
    }
}
