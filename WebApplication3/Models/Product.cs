using System;
using System.Collections.Generic;

namespace WebApplication3.Models;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public DateTime? CreatedDate { get; set; }
}
