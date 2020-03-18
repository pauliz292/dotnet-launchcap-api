using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Guarantor
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