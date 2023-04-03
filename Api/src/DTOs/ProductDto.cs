using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.src.DTOs
{
    public class ProductBaseDto
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Price { get; set; }
         public ICollection<string> ImageUrl { get; set; } = null!;
    }

    public class ProductReadDto : ProductBaseDto { }

    public class ProductCreateDto : ProductBaseDto { }

    public class ProductUpdateDto : ProductBaseDto { }
}
