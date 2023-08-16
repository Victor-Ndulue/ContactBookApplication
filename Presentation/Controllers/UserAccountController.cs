using Microsoft.AspNetCore.Mvc;
using Service.Contracts.IEntityServices;
using Shared.DTOs;
using Shared.DTOs.UserDTOs;

namespace Presentation.Controllers
{
    public class UserAccountController : BaseController
    {
        private readonly IUserAccountService _userAccountService;
        public UserAccountController(IUserAccountService userAccountService)
        {
            _userAccountService = userAccountService;
        }

        [HttpPost]
        [Route("user/create")]
        public async Task<IActionResult> CreateUserAccount(UserDtoForCreation userDtoForCreation)
        {
            var result = await _userAccountService.CreateUser(userDtoForCreation);
            return Ok(result);
        }

        [HttpPost]
        [Route("user/login")]
        public async Task<IActionResult> UserLogin(UserDtoForLogin userDtoForLogin) 
        {
            var result =await _userAccountService.Login(userDtoForLogin);
            return Ok(result);
        }
    }
}
