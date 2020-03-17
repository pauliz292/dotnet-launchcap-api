using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Property
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(250)]
        public string Address { get; set; }
    }
}