using Service.Contracts.IEntityServices;

namespace Presentation.Controllers
{
    public class UserLoginController : BaseController
    {
        private readonly IUserLoginService _userLoginService;
        public UserLoginController()
        {
        }
    }
}
