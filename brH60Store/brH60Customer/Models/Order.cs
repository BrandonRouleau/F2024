using System;
using System.Collections.Generic;

namespace brH60Customer.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int CustomerId { get; set; }

    public DateOnly DateCreated { get; set; }

    public DateOnly? DateFulfilled { get; set; }

    public decimal? Total { get; set; }

    public decimal? Taxes { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
