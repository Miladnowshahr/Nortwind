using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login(UserForLoginDto userForLoginDto)
        {
            var userToLogin = _authService.Login(userForLoginDto);

            if (userToLogin.Success is false)
            {
                return BadRequest(userToLogin.Message);
            }
            var result = _authService.CreateAccessToke(userToLogin.Data);

            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("register")]
        public IActionResult Register(UserForRegisterDto userForRegisterDto)
        {
            var isExist = _authService.UserExist(userForRegisterDto.Email);

            if (isExist.Success is false)
            {
                return BadRequest(isExist.Message);
            }

            var resultRegister = _authService.Register(userForRegisterDto,userForRegisterDto.Password);

            if (resultRegister.Success is false)
            {
                return BadRequest(resultRegister.Message);
            }

            var result = _authService.CreateAccessToke(resultRegister.Data);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
    }
}
