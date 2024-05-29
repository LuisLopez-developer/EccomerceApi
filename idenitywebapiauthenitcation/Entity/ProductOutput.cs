namespace EccomerceApi.Entity
{
    public class ProductOutput
    {
        public int Id {  get; set; }
        public DateTime? Date { get; set; }

        public int ReasonForExitId { get; set; }
        public virtual ReasonForExit ReasonForExit{ get; set; }

        public int SaleDetailId { get; set; }
        public virtual SaleDetail SaleDetail { get; set; }
    }
}
