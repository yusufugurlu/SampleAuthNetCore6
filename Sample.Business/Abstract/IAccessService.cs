using Sample.Common.Result;
using Sample.DataAccess.Entities;
using Sample.Models.UserViewModel.ViewModel;
using Sample.Models.ViewModel.UserViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Business.Abstract
{
    public interface IAccessService
    {
        ServiceResult AddUser(AddUserViewModel addUserView);
        ServiceResult Login(LoginUserViewModel loginUserView);
        ServiceResult AddUserActivatorMail(User addUserView);
        ServiceResult VerifyActivationMail(VerifyActivationMailViewModel verifyActivationMailView);
        void AddLoginResponseTimeStamp(UserLoginResponseTimeStamp userLoginResponseTimeStamp);
        
    }
}
