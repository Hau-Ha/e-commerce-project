using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.src.Models
{
    public class Order: BaseModel
    {
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
    
        public double TotalPrice { get; set; }
    }
}