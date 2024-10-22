namespace Models;

public partial class EntryModel
{
    public int Id { get; set; }

    public DateTime? Date { get; set; }

    public decimal? Total { get; set; }

    public int? EntryTypeId { get; set; }
    public virtual EntryTypeModel? EntryType { get; set; }


    public int? StateId { get; set; }
    public virtual StateModel? State { get; set; }

    public virtual ICollection<EntryDetailModel> EntryDetails { get; set; } = new List<EntryDetailModel>();
}
