using AAEICS.GuardFS.etc;

namespace AAEICS.GuardFS.Contracts;

public interface ICheckupStep
{
    public string Name { get; set; }
    public string Description { get; set; }

    public CheckupStepResultForm Execute();
}