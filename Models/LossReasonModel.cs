namespace Models
{
    public class LossReasonModel
    {
        public int Id { get; set; }
        public required string Reason { get; set; }

        public virtual ICollection<LostDetailModel> LostDetails { get; set; } = new List<LostDetailModel>();

    }
}
