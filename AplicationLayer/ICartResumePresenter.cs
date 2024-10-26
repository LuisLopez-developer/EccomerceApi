using EnterpriseLayer;

namespace AplicationLayer
{
    public interface ICartResumePresenter<TOutput>
    {
        public TOutput Present(Cart cart, IEnumerable<Product> products);
    }
}
