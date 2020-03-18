using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Application.Property;
using Application.User;

namespace Application.Guarantor
{
    public class GuarantorDto
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        public string ContactNumber { get; set; }

        public string Email { get; set; }

        public string Company { get; set; }
    }
}