﻿using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace BookStoreLibrary.Entities.Models
{
    public class Role : IdentityRole<int>
    {
        public ICollection<UserRole> User { get; set; }

    }
}
