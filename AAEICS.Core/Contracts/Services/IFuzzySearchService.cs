namespace AAEICS.Core.Contracts.Services;

public interface IFuzzySearchService<T> where T : class
{
    public void LoadData(IEnumerable<T> data);
    public IEnumerable<T> Search(string searchTerm, Func<T, string> propertySelector, int maxDistance = 3, int topCount = 5);
    public void AddItemToCache(T newItem);
    public void RemoveItemFromCache(T itemToRemove);
}
