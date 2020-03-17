using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Application.Property;
using Application.User;

namespace Application.Borrower
{
    public class BorrowerDto
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(250)]
        public string Address { get; set; }

        public string Email { get; set; }

        public string ContactNumber { get; set; }

        public string ACN { get; set; }

        public virtual List<UserDetailDto> Users { get; set; }

        public virtual List<PropertyDto> Properties { get; set; }
    }
}