namespace AAEICS.Core.Contracts.Repositories;

public interface IGenericRepository<TDto> where TDto : class
{
    Task<IEnumerable<TDto>> GetAllAsync();
    Task<TDto?> GetByIdAsync(object id);
    Task AddAsync(TDto dto);
    Task UpdateAsync(TDto dto);
    Task RemoveAsync(TDto dto);
}
