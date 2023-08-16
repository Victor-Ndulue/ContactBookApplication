using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs.UserDTOs
{
    public class UserDtoForLogin
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
