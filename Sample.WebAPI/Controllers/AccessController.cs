using Microsoft.AspNetCore.Mvc;
using Sample.Business.Abstract;
using Sample.Business.Operations.UserOperation;
using Sample.Models.UserViewModel.ViewModel;
using Sample.Models.ViewModel.UserViewModel;

namespace Sample.WebAPI.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class AccessController : ControllerBase
    {
        private readonly IAccessService _accessService;
        public AccessController(IAccessService accessService)
        {
            _accessService = accessService;
        }
        [HttpPost]
        public IActionResult AddUser(AddUserViewModel model)
        {
            AddUserOperation addUserOperation = new AddUserOperation(_accessService);
            addUserOperation.AddUserViewModel = model;
            var result = addUserOperation.Handle();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Login(LoginUserViewModel model)
        {
            LoginUserOperation addUserOperation = new LoginUserOperation(_accessService);
            addUserOperation.LoginUserViewModel = model;
            var result = addUserOperation.Handle();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult VerifyActivationMail(VerifyActivationMailViewModel model)
        {
            VerifyActivationMailOperation addUserOperation = new VerifyActivationMailOperation(_accessService);
            addUserOperation.VerifyActivationMailModel = model;
            var result = addUserOperation.Handle();
            return Ok(result);
        }
    }
}
