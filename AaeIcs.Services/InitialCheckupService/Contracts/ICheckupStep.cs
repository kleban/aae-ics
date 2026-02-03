using AAEICS.Services.InitialCheckupService.CheckupForm;

namespace AAEICS.Services.InitialCheckupService.Contracts;

public interface ICheckupStep
{
    public string Name { get; set; }
    public string Description { get; set; }
    public CheckupStepResultForm Execute();
}