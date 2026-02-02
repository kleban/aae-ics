using AAEICS.GuardFS.Contracts;

namespace AAEICS.GuardFS.Core;

public class GuardEngine(IEnumerable<ICheckup> checkups)
{
    private readonly List<ICheckup> _checkups = checkups.ToList();

    public async Task ShieldUpAsync()
    {

        foreach (var checkup in _checkups)
        {
            var result = checkup.Execute();

            // Якщо IsSuccessful == false (FailedSteps.Count > 0), це "червоне" світло
            if (!result.IsSuccessful)
            {
                // Формуємо текст помилки для виключення
                var errorMsg = string.Join(Environment.NewLine, result.Messages ?? []);
                throw new Exception($"GuardFS Critical Stop [{checkup.Name}]: {errorMsg}");
            }

            // Якщо є Warnings, виводимо їх у консоль або лог, але йдемо далі
            if (result.HasWarnings)
            {
                foreach (var msg in result.Messages ?? [])
                {
                    Console.WriteLine($"[GuardFS Warning]: {msg}");
                }
            }
        }
    }
}