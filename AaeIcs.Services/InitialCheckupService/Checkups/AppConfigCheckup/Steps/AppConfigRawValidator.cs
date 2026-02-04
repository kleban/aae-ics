
using AAEICS.Services.InitialCheckupService.CheckupForm;
using AAEICS.Services.InitialCheckupService.Contracts;
using System.Text.Json;

namespace AAEICS.Services.InitialCheckupService.Checkups.AppConfigCheckup.Steps;

public class AppConfigRawValidator(string name, string description, string appsettings_path) : ICheckupStep
{
    public string Name { get; set; } = name;
    public string Description { get; set; } = description;

    public CheckupStepResultForm Execute()
    {
        try
        {
            if (!File.Exists(appsettings_path))
            {
                return new CheckupStepResultForm
                {
                    Result = CheckupStepResults.Failed,
                    Message = "Configuration file missing during raw validation."
                };
            }

            string content = File.ReadAllText(appsettings_path);

            if (string.IsNullOrWhiteSpace(content))
            {
                return new CheckupStepResultForm
                {
                    Result = CheckupStepResults.Failed,
                    Message = "The appsettings.json file is empty."
                };
            }

            // Перевіряємо, чи це валідний JSON взагалі
            using var jsonDoc = JsonDocument.Parse(content);
            
            // Перевіряємо наявність секції Database, яку ви згадували
            if (!jsonDoc.RootElement.TryGetProperty("Database", out _))
            {
                return new CheckupStepResultForm
                {
                    Result = CheckupStepResults.Warning,
                    Message = "JSON is valid, but 'Database' section is missing. Will use defaults."
                };
            }

            return new CheckupStepResultForm
            {
                Result = CheckupStepResults.Successful,
                Message = "JSON structure is valid and contains required sections."
            };
        }
        catch (JsonException)
        {
            return new CheckupStepResultForm
            {
                Result = CheckupStepResults.Failed,
                Message = "The appsettings.json file has invalid JSON format."
            };
        }
        catch (Exception ex)
        {
            return new CheckupStepResultForm
            {
                Result = CheckupStepResults.Failed,
                Message = $"Unexpected error during config validation: {ex.Message}"
            };
        }
    }
}