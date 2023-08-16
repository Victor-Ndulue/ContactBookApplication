using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs.ContactDTOs
{
    public class ContactDtoForUpdate
    {        
        public string ContactName { get; set; }
        public string ContactPhoneNumber { get; set; }
        public string ContactEmail { get; set; }
        public IFormFile PhotoFile { get; set; }    
    }
}
