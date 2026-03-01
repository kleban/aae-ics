namespace AAEICS.Client.Messages;

public class CreateNewItemMessage<T>(string searchText, Action<T> onItemCreated)
    where T : class
{
    public string SearchText { get; } = searchText;
    public Action<T> OnItemCreated { get; } = onItemCreated;
}