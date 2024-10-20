namespace Mappers.Dtos.Requests
{
    public class OrderFilterDTO
    {
        public string? CustomerDNI { get; set; }
        public int? StatusId { get; set; }
        public int? PaymentMethodId { get; set; }
        public DateTime? CreatedFrom { get; set; }
        public DateTime? CreatedTo { get; set; }
        public string? WorkerId { get; set; }
    }
}
