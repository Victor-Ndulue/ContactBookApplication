using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs.ContactDTOs
{
    public class ContactDtoForCreation
    {
        [Required(ErrorMessage ="Contact name cannot be null")]
        public string ContactName { get; set; }
        [Required(ErrorMessage = "Contact phonenumber cannot be null")]
        public string ContactPhoneNumber { get; set; }
        public string? ContactEmail { get; set; }
    }
}
