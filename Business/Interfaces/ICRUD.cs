namespace Business.Interfaces
{
    public interface ICRUD<TModel> where TModel : class
    {
        IEnumerable<TModel> GetAll();

        Task<TModel?> GetByIdAsync(int id);

        Task AddAsync(TModel model);

        Task UpdateAsync(TModel model);

        Task DeleteByIdAsync(int id);
    }
}
