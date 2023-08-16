﻿using Microsoft.AspNetCore.Identity;

namespace Models.Entities
{
    public class AppUserRole : IdentityUserRole<int>
    {
        public User User { get; set; }
        public AppRole Role { get; set; }
    }
}
