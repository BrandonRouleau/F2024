using System;
using System.Collections.Generic;

namespace brH60Store.Models;

public partial class ProductCategory
{
    public int CategoryId { get; set; }

    public string ProdCat { get; set; } = null!;

    public string? Image {  get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
