using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs.ContactDTOs
{
    public class ContactDtoForUpdate
    {        
        public string NewContactName { get; set; }
        public string NewContactPhoneNumber { get; set; }
        public string NewContactEmail { get; set; }   
    }
}
