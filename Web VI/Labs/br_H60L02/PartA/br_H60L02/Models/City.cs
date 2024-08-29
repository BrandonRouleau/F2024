using System;
using System.Collections.Generic;

namespace br_H60L02.Models;

public partial class City
{
    public decimal CityId { get; set; }

    public string? City1 { get; set; }

    public virtual ICollection<Person> People { get; set; } = new List<Person>();
}
