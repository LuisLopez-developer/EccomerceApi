namespace AplicationLayer
{
    public interface IGetRepository<TOutput>
    {
        Task<IEnumerable<TOutput>> GetAllAsync();
    }
}
