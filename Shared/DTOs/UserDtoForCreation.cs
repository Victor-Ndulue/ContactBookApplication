using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs
{
    public class UserDtoForCreation
    {
        [Required(ErrorMessage = "The userName field is required.")]
        [StringLength(15, MinimumLength = 5, ErrorMessage = "The userName field must be between 5 and 15 characters.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "The email field is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Invalid phone number format.")]
        public string PhoneNumber { get; set; }
    }
}
