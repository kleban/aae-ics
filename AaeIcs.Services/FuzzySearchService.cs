using AAEICS.Core.Contracts.Services;

using FuzzySharp;

namespace AAEICS.Services;

public class FuzzySearchService<T>: IFuzzySearchService<T> where T : class
{
    private List<T> _cachedData = new ();
    
    public void LoadData(IEnumerable<T> data)
    {
        _cachedData = data?.ToList() ?? new List<T>();
    }
    
    public IEnumerable<T> Search(string searchTerm, Func<T, string> propertySelector, int maxDistance = 2, int topCount = 5)
    {
        if (string.IsNullOrWhiteSpace(searchTerm)) 
            return _cachedData.Take(topCount).ToList();

        var lowerSearchTerm = searchTerm.ToLowerInvariant();

        // Мінімальний бал для FuzzySharp (від 0 до 100).
        // 60 - це хороше стартове значення для допуску кількох помилок.
        int minFuzzyScore = 60; 

        return _cachedData
            .Select(item =>
            {
                var text = propertySelector(item)?.ToLowerInvariant() ?? string.Empty;
                int score = int.MaxValue; // int.MaxValue означає, що збігу немає взагалі

                // 1. Точний збіг (Пріоритет №1)
                if (text == lowerSearchTerm) 
                    score = 0; 
                    
                // 2. Слово починається з введених літер (Пріоритет №2)
                else if (text.StartsWith(lowerSearchTerm)) 
                    score = 1; 
                    
                // 3. Слово містить введені літери (Пріоритет №3)
                else if (text.Contains(lowerSearchTerm)) 
                    score = 2; 
                    
                // 4. Непрямий пошук (опечатки) за допомогою FuzzySharp
                else 
                {
                    // Fuzz.WeightedRatio автоматично підбирає найкращий алгоритм порівняння.
                    // Він добре обробляє як опечатки, так і переплутані місцями слова.
                    int fuzzyMatchScore = Fuzz.WeightedRatio(lowerSearchTerm, text);
                    
                    // Якщо бал вищий за наш поріг, приймаємо цей варіант
                    if (fuzzyMatchScore >= minFuzzyScore)
                    {
                        // Перетворюємо 100-бальну систему на твою (чим менше, тим краще).
                        // Якщо fuzzyMatchScore = 100, score буде 10 (найкращий серед нечітких).
                        // Якщо fuzzyMatchScore = 60, score буде 50 (найгірший з допустимих).
                        score = 10 + (100 - fuzzyMatchScore);
                    }
                }

                return new { Item = item, Score = score, Text = text };
            })
            .Where(x => x.Score != int.MaxValue) // Відкидаємо те, що не пройшло жодну перевірку
            .OrderBy(x => x.Score)               // Сортуємо: спочатку точні збіги, потім Fuzzy
            .ThenBy(x => x.Text)                 // Алфавітне сортування при однакових балах
            .Select(x => x.Item)
            .Take(topCount)
            .ToList();
    }
    
    public void AddItemToCache(T newItem)
    {
        if (newItem != null) _cachedData.Add(newItem);
    }
    
    public void RemoveItemFromCache(T itemToRemove)
    {
        if (itemToRemove != null) _cachedData.Remove(itemToRemove);
    }
    
    private int CalculateLevenshteinDistance(string source, string target)
    {
        if (string.IsNullOrEmpty(source)) return target?.Length ?? 0;
        if (string.IsNullOrEmpty(target)) return source.Length;

        int sourceLength = source.Length;
        int targetLength = target.Length;
        var distance = new int[sourceLength + 1, targetLength + 1];

        for (int i = 0; i <= sourceLength; distance[i, 0] = i++) { }
        for (int j = 0; j <= targetLength; distance[0, j] = j++) { }
        
        for (int i = 1; i <= sourceLength; i++)
        {
            for (int j = 1; j <= targetLength; j++)
            {
                int cost = (target[j - 1] == source[i - 1]) ? 0 : 1;

                distance[i, j] = Math.Min(
                    Math.Min(distance[i - 1, j] + 1, distance[i, j - 1] + 1),
                    distance[i - 1, j - 1] + cost);
            }
        }

        return distance[sourceLength, targetLength];
    }
}
