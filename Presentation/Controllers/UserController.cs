using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.PaginationDefiners;

namespace Presentation.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [AllowAnonymous]
        [HttpGet]
        [Route("users")]
        public async Task<IActionResult> GetAllUsers([FromQuery] PaginationParams paginationParams, bool trackChanges)
        {
            var result = await _userService.GetAllUsers(paginationParams, trackChanges);
            return Ok(result);
        }

        [HttpGet]
        [Route("userbyid/{id}")]
        public async Task<IActionResult> GetUserById(int id, bool trackChanges)
        {
            var result = await _userService.GetUserById(id, trackChanges);
            return Ok(result);
        }

        [HttpGet]
        [Route("userbymail/{email}")]
        public async Task<IActionResult> GetUserByEmail(string email, bool trackChanges)
        {
            var result = await _userService.GetUserByEmail(email, trackChanges);
            return Ok(result);
        }
    }
}
