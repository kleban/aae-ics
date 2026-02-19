namespace AAEICS.Client.Services;

public interface IWindowController
{
    // Ручки для керування меню
    void ShowMenu();
    void HideMenu();

    void RegisterPageMinWidth(double minWidth);
    void CheckLayoutRules(double currentWindowWidth);
        
    // Метод, щоб дізнатись, чи меню зараз відкрите
    bool IsMenuVisible { get; }
}