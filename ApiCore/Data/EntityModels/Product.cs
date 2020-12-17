using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiCore.Data.EntityModels
{
    
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        public List<OrderProduct> OrderProducts { get; set; }

        public DateTime CreationDateTime { get; set; } = DateTime.Now;
    }
}
