using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagement.Models
{
    public class Product
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public required decimal Price { get; set; }

        public int CategoryId { get; set; } //primary key reference to Category
        public Category Category { get; set; } //navigation property (Point back to the single Category)

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}