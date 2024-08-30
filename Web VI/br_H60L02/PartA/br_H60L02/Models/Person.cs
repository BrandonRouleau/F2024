using System;
using System.Collections.Generic;

namespace br_H60L02.Models;

public partial class Person
{
    public decimal PersonId { get; set; }

    public string? LastName { get; set; }

    public string? FirstName { get; set; }

    public decimal CityId { get; set; }

    public virtual City City { get; set; } = null!;
}
