using System;
using System.Collections.Generic;

namespace Application.Product
{
    public class ProductDto
    {
        public int Id { get; set; }
        
        public int CategoryId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImagePath { get; set; }
    }
}
