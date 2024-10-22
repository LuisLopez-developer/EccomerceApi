namespace Models;
public partial class StateModel
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<EntryModel> Entries { get; set; } = new List<EntryModel>();

    public virtual ICollection<LossModel> Losses { get; set; } = new List<LossModel>();

    public virtual ICollection<ProductModel> Products { get; set; } = new List<ProductModel>();

    public virtual ICollection<UserModel> AspNetUsers { get; set; } = new List<UserModel>();
}
