using Microsoft.Extensions.DependencyInjection;
using AAEICS.Core.Contracts.Repositories;
using AAEICS.Core.Contracts.Services;

namespace AAEICS.Services;

public class DictionaryDataService(IServiceProvider serviceProvider) : IDictionaryDataService
{
    public async Task<IEnumerable<TDto>> GetAllDataAsync<TEntity, TDto>() 
        where TEntity : class 
        where TDto : class
    {
        var repository = serviceProvider.GetService<IGenericRepository<TDto>>();
        if (repository == null) throw new InvalidOperationException($"Репозиторій для {typeof(TDto).Name} не знайдено!");
        
        return await repository.GetAllAsync();
    }

    public async Task AddDataAsync<TEntity, TDto>(TDto dto) 
        where TEntity : class 
        where TDto : class
    {
        var repository = serviceProvider.GetService<IGenericRepository<TDto>>();
        if (repository == null) throw new InvalidOperationException($"Репозиторій для {typeof(TDto).Name} не знайдено!");
        
        await repository.AddAsync(dto);
        // Залежно від архітектури, тут може знадобитися виклик UnitOfWork.CompleteAsync()
    }
}