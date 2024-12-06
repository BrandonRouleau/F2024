using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace brH60Services.Models;

public partial class ProductCategory
{
    public int CategoryId { get; set; }

    public string ProdCat { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
