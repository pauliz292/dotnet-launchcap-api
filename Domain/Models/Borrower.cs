using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class Borrower
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

        // foreign keys
        public virtual ICollection<AppUser> Users { get; set; }

        public virtual ICollection<Property> Properties { get; set; }
    }
}