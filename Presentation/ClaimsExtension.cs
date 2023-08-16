using System.Security.Claims;

namespace Presentation
{
    public static class ClaimsExtension
    {
        public static string GetUserEmail(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.Email).Value;
        }
    }
}
