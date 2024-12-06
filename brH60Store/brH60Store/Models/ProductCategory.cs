using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace brH60Store.Models;

public partial class ProductCategory
{
    public int CategoryId { get; set; }

    public string ProdCat { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
