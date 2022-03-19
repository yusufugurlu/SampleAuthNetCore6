using Sample.Business.Abstract;
using Sample.Common.Response;
using Sample.Models.ViewModel.UserViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Business.Operations.UserOperation
{
    public class VerifyActivationMailOperation
    {
        public VerifyActivationMailViewModel VerifyActivationMailModel { get; set; }
        public readonly IAccessService _accessService;
        public VerifyActivationMailOperation(IAccessService accessService)
        {
            _accessService = accessService;
        }

        public ResponseModel Handle()
        {
            ResponseModel model = new();

            var result = _accessService.VerifyActivationMail(VerifyActivationMailModel);
            if (result.IsSuccess)
            {
                model.Data = result.Data;
                model.HasData = true;

            }
            model.Message = result.Message;
            model.Status = result.Status();
            return model;
        }
    }
}
