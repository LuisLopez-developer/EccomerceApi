using System;
using System.Collections.Generic;

namespace EccomerceApi.Entity;
public partial class State
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Entry> Entries { get; set; } = new List<Entry>();

    public virtual ICollection<Loss> Losses { get; set; } = new List<Loss>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
