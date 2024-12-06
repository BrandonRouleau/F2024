using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace brH60Services.Models;

public partial class ShoppingCart
{
    public int CartId { get; set; }

    public int CustomerId { get; set; }

    public DateTime DateCreated { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual Customer Customer { get; set; } = null!;
}
