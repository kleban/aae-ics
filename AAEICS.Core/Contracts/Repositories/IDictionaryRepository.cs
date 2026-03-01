namespace AAEICS.Core.Contracts.Repositories;

public interface IDictionaryRepository
{
    Task<IEnumerable<TDto>> GetAllAsync<TEntity, TDto>() 
        where TEntity : class 
        where TDto : class;
    Task AddAsync<TEntity, TDto>(TDto dto) 
        where TEntity : class 
        where TDto : class;
}
