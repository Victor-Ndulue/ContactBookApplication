﻿using Microsoft.AspNetCore.Identity;

namespace Models.Entities
{
    public class User : IdentityUser<int>
    {
        public ICollection<Contact> Contacts { get; set; }
        public ICollection<AppUserRole> UserRoles { get; set; }
    }
}
