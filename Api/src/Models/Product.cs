using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Api.src.Models
{
    public class Product : BaseModel
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        [Column(TypeName = "jsonb")]
        public ICollection<string> ImageUrl { get; set; } = null!;
    }
}