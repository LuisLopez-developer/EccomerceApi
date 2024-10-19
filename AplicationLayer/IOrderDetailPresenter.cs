using EnterpriseLayer;

namespace AplicationLayer
{
    public interface IOrderDetailPresenter<TOutput>
    {
        TOutput Present(Order order, IEnumerable<Product> products);
    }
}
