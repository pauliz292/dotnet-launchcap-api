using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Domain.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool isDeleted { get; set; }

        public virtual List<Product> Products { get; set; }
    }
}