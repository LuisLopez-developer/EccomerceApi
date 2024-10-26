namespace Mappers.Dtos.Requests
{
    public class ChangeItemQuantityDTO
    {
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public ChangeAction Action { get; set; }

    }

    public enum ChangeAction
    {
        increase_quantity,
        decrease_quantity
    }
}
