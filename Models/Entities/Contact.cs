using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Models.Entities
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }
        [PersonalData]
        public string ContactName { get; set; }
        [PersonalData]
        public string ContactPhoneNumber { get; set; }
        [PersonalData]
        public string ContactEmail { get; set; }
        [PersonalData]
        public ICollection<Photo> Photos { get; set; }
    }
}
