using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagement.DTO
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
    }

    public class CreateCategoryDTO
    {
        public required string Name { get; set; }   
        public string Description { get; set; }    
    }
}