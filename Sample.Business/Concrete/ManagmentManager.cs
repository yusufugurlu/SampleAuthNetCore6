using AutoMapper;
using Sample.Business.Abstract;
using Sample.Business.GenericRepository;
using Sample.Common.Result;
using Sample.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Business.Concrete
{
    public class ManagmentManager : IManagmentService
    {
        private readonly IUnitOfWorks _unitOfWorks;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<User> _userRepo;
        private readonly IGenericRepository<UserActivationEmailInformation> _userActivationRepo;

        public ManagmentManager(IUnitOfWorks unitOfWorks, IMapper mapper)
        {
            _unitOfWorks = unitOfWorks;
            _mapper = mapper;
            _userRepo = unitOfWorks.GetGenericRepository<User>();
            _userActivationRepo = unitOfWorks.GetGenericRepository<UserActivationEmailInformation>();
        }
        public ServiceResult GetNewRegisterUserOneDay()
        {
         var registerUserCount=   _userRepo.GetAll(x => !x.IsDisabled && (x.RegistrationDate.HasValue ? (x.RegistrationDate.Value.Month == DateTime.Now.Month
            && x.RegistrationDate.Value.Day == DateTime.Now.Day
            && x.RegistrationDate.Value.Year == DateTime.Now.Year) : false
            )).Count();

            return Result.Success(true,"", registerUserCount);
        }

        public ServiceResult GetOnlineUser()
        {
            var onlineUserCount = _userRepo.GetAll(x => !x.IsDisabled && x.IsLogin).Count();

            return Result.Success(true, "", onlineUserCount);
        }

        public ServiceResult GetSendActivitionButNotUsedOneDay()
        {
            var expiryActivationOneDay = _userActivationRepo.GetAll(x => !x.IsUsed && !x.IsDisabled && x.ExpiryDate > DateTime.Now.AddDays(1).Date).Count();

            return Result.Success(true, "", expiryActivationOneDay);
        }
    }
}
