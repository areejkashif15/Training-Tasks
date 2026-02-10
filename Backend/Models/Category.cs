using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagement.Models
{
    public class Category
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; } // ? -> nullable

        public ICollection<Product> Products { get; set; } = new List<Product>(); //navigation property (Point to multiple Products)
        // collection Navigation Property (One category has many products) 
        //ICollection -> define the intent (a group of items that can be added to or counted) without dictating exactly how they are stored.

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}