using Core.Concrete;
using Core.Utility.Results;
using Core.Utility.Security.JWT;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<User> Register(UserForRegisterDto registerDto, string password);
        IDataResult<AccessToken> CreateAccessToke(User user);
        IDataResult<User> Login(UserForLoginDto loginDto);
        IResult UserExist(string email);
    }
}
