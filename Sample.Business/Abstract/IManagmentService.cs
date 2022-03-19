using Sample.Common.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Business.Abstract
{
    public interface IManagmentService
    {
        ServiceResult GetOnlineUser();
        ServiceResult GetNewRegisterUserOneDay();
        ServiceResult GetSendActivitionButNotUsedOneDay();
        ServiceResult GetLoginRequestTimeStampAverrage();
    }
}
