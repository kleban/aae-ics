namespace AAEICS.Core.Contracts.Services;

public interface IDictionaryDataService
{
    Task<IEnumerable<TDto>> GetAllDataAsync<TEntity, TDto>() 
        where TEntity : class 
        where TDto : class;
    Task AddDataAsync<TEntity, TDto>(TDto dto) 
        where TEntity : class 
        where TDto : class;
}
