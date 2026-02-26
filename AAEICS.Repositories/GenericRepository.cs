using System.Linq.Expressions;
using AAEICS.Core.Contracts.Repositories;
using AAEICS.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace AAEICS.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly AAEICSDbContext DbContext;
    protected readonly DbSet<T> DbSet;

    public GenericRepository(AAEICSDbContext context)
    {
        DbContext = context;
        DbSet = DbContext.Set<T>();
    }

    // Віртуальний метод: нащадки можуть перевизначити його, щоб додати свої .Include()
    protected virtual IQueryable<T> GetBaseQuery()
    {
        return DbSet;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        // Використовуємо GetBaseQuery() замість DbSet
        return await GetBaseQuery().ToListAsync();
    }

    public async Task<T?> GetByIdAsync(object id)
    {
        // Використовуємо GetBaseQuery() замість DbSet
        var query = GetBaseQuery();

        var entityType = DbContext.Model.FindEntityType(typeof(T));
        var primaryKey = entityType?.FindPrimaryKey();
        var keyName = primaryKey?.Properties.Select(x => x.Name).FirstOrDefault();

        if (keyName == null) return null;

        return await query.FirstOrDefaultAsync(e => EF.Property<object>(e, keyName).Equals(id));
    }

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        // Використовуємо GetBaseQuery() замість DbSet
        return await GetBaseQuery().Where(predicate).ToListAsync();
    }

    public async Task AddAsync(T entity)
    {
        await DbSet.AddAsync(entity);
    }

    public async Task UpdateAsync(T entity)
    {
        DbSet.Update(entity);
        await Task.CompletedTask;
    }

    public async Task RemoveAsync(T entity)
    {
        DbSet.Remove(entity);
        await Task.CompletedTask;
    }
}