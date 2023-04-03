using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Api.src.Models
{
    public class Category : BaseModel
    {
        public string Name { get; set; } = null!;
        public string Image { get; set; } = null!;
        public ICollection<Product> Products { get; set; } = null!;
    }
}