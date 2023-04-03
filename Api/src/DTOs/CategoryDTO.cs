using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.src.DTOs
{
   public class CategoryBaseDTO
{
    public string Name { get; set; } = null!;
    public string Image { get; set; } = null!;
}

public class CategoryReadDTO : CategoryBaseDTO
{
}

public class CategoryCreateDTO : CategoryBaseDTO
{
}

public class CategoryUpdateDTO : CategoryBaseDTO
{
}
}