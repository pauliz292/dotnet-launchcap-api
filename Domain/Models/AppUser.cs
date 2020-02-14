using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public enum Gender
    {
        Male = 1,
        Female
    }

    public class AppUser : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public DateTime Birthday { get; set; }
        public string Occupation { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}