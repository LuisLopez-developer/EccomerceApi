namespace Data.Entity;
public partial class State
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Entry> Entries { get; set; } = new List<Entry>();

    public virtual ICollection<Loss> Losses { get; set; } = new List<Loss>();

    public virtual ICollection<ProductModel> Products { get; set; } = new List<ProductModel>();

    public virtual ICollection<AppUser> AspNetUsers { get; set; } = new List<AppUser>();
}
