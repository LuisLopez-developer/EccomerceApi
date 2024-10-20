namespace Presenters.SaleViewModel
{
    public class StatusViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public StatusViewModel(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
