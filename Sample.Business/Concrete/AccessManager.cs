using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Sample.Business.Abstract;
using Sample.Business.GenericRepository;
using Sample.Business.Operations.TokenOperation;
using Sample.Common.Helper;
using Sample.Common.Result;
using Sample.DataAccess.Entities;
using Sample.Models.UserViewModel.ViewModel;
using Sample.Models.ViewModel.UserViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Business.Concrete
{
    public class AccessManager : IAccessService
    {
        private readonly IUnitOfWorks _unitOfWorks;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<User> _userRepo;
        private readonly IGenericRepository<UserActivationEmailInformation> _userActivationRepo;
        private readonly IGenericRepository<UserLoginResponseTimeStamp> _userLoginResponseTimeStampRepo;
        private readonly IConfiguration _config;
        public AccessManager(IUnitOfWorks unitOfWorks, IMapper mapper, IConfiguration config)
        {
            _unitOfWorks = unitOfWorks;
            _mapper = mapper;
            _config = config;
            _userRepo = _unitOfWorks.GetGenericRepository<User>();
            _userActivationRepo = _unitOfWorks.GetGenericRepository<UserActivationEmailInformation>();
            _userLoginResponseTimeStampRepo = _unitOfWorks.GetGenericRepository<UserLoginResponseTimeStamp>();
        }

        public void AddLoginResponseTimeStamp(UserLoginResponseTimeStamp userLoginResponseTimeStamp)
        {
            _userLoginResponseTimeStampRepo.Add(userLoginResponseTimeStamp);
            _unitOfWorks.SaveChanges();
        }

        public ServiceResult AddUser(AddUserViewModel addUserView)
        {
            var user = _mapper.Map<User>(addUserView);
            _userRepo.Add(user);
            var result = _unitOfWorks.SaveChanges();

            if (result.IsSuccess)
            {
                AddUserActivatorMail(user);
                var message = "Kullanıcı oluşturuldu. Sisteme giriş yapmak için e-postanıza gelen aktivasyon kodunu etkinleştiriniz.";
                return Result.Success( message);
            }
            return result;
        }

        public ServiceResult AddUserActivatorMail(User addUserView)
        {
            var guidGenerator = GuidGenerators.GuidGenerator().Replace("-", "");
            var webRootUrl = "";
            var urlActivationCode = "";
            SendEmails.SendEmail(new Models.SendEmail.SendEmailDto()
            {
                ToEmail = addUserView.EMail,
                Message = urlActivationCode
            });

            _userActivationRepo.Add(new UserActivationEmailInformation
            {
                ExpiryDate = DateTime.Now.AddHours(12),
                UserId = addUserView.Id,
                GuidKey = guidGenerator,
            });

            return _unitOfWorks.SaveChanges();
        }

        public ServiceResult Login(LoginUserViewModel loginUserView)
        {
            var user = _userRepo.GetAll(x => x.EMail == loginUserView.EMail && x.Password == loginUserView.Password && !x.IsDisabled).FirstOrDefault();
            if (user != null)
            {
                if (user.IsActivationEmail)
                {
                    var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                };

                    TokenHanddler handler = new TokenHanddler(_config);
                    Token token = handler.CreateAccessToken(user,authClaims);
                    user.RefreshToken = token.RefreshToken;
                    user.RefreshTokenExpiryDate = token.Expiration.AddMinutes(5);
                    user.IsLogin = true;
                    _userRepo.Update(user);
                    _unitOfWorks.SaveChanges();
                    return Result.Success( "", token);
                }
                else
                {
                    return Result.Fail( "Emailinii aktive etmelisiniz.");
                }
            }
            return Result.Fail( "Kullanıcı bulunamadı.");
        }

        public ServiceResult VerifyActivationMail(VerifyActivationMailViewModel verifyActivationMailView)
        {
            var userActivation = _userActivationRepo.GetAll(x => x.GuidKey == verifyActivationMailView.ActivacitonKey && !x.IsDisabled && !x.IsUsed && !x.User.IsActivationEmail).OrderByDescending(x=>x.Id).FirstOrDefault();
            if (userActivation != null)
            {
                if (userActivation.ExpiryDate >= DateTime.Now)
                {
                    var user = _userRepo.Get(userActivation.UserId);
                    user.IsActivationEmail = true;
                    user.RegistrationDate = DateTime.Now;
                    _userRepo.Update(user);
                    userActivation.IsUsed = true;
                    userActivation.UsedDate = DateTime.Now;
                    _userActivationRepo.Update(userActivation);
                    _unitOfWorks.SaveChanges();
                    return Result.Success( "Aktivasyon edildi.");
                }
                else
                {
                    return Result.Fail( "Kod geçerlilik süresi doldu");
                }
            }

            return Result.Fail( "Kod geçersiz");
            throw new NotImplementedException();
        }
    }
}
