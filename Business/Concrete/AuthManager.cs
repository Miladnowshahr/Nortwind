using Business.Abstract;
using Business.Constant;
using Core.Concrete;
using Core.Utility.Results;
using Core.Utility.Security.Hashing;
using Core.Utility.Security.JWT;
using Entities.Dtos;

namespace Business.Concrete
{
   public class AuthManager:IAuthService
    {
        private IUserService _userService;
        private ITokenHelper _tokenHelper;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }

        public IDataResult<User> Register(UserForRegisterDto registerDto,string password)
        {
            byte[] passwordHash, passwordSalt;

            HashHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            var user = new User
            {
                Email = registerDto.Email,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Status = true,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };
            _userService.Add(user);
            return new SuccessDataResult<User>(user,Message.UserRegister,Message.UserRegisterId);
        }

        public IDataResult<User> Login(UserForLoginDto loginDto)
        {
            var userCheck = _userService.GetByEmail(loginDto.Email);
            if (userCheck==null)
            {
                return new ErrorDataResult<User>(Message.UserNotFound,Message.UserNotFoundId);
            }

            if (HashHelper.VerifyPasswordHash(loginDto.Password,userCheck.PasswordHash,userCheck.PasswordSalt) is false)
            {
                return new ErrorDataResult<User>(Message.PasswordWrong,Message.PasswordWrongId);
            }
            return new SuccessDataResult<User>(userCheck, Message.SuccessLogin,Message.SuccessLoginId);
        }

        public IDataResult<AccessToken> CreateAccessToke(User user)
        {
            var claim = _userService.GetClaim(user);
            var accessToken = _tokenHelper.CreateToken(user, claim);
            return new SuccessDataResult<AccessToken>(accessToken);
        }

        public IResult UserExist(string email)
        {
            if (_userService.GetByEmail(email)!=null)
            {
                return new ErrorResult(Constant.Message.UserExist,Message.UserExistId);
            }
            return new SuccesessResult();
        }
    }
}
