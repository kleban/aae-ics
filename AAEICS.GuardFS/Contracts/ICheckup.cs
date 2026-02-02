using AAEICS.GuardFS.etc;

namespace AAEICS.GuardFS.Contracts;

public interface ICheckup
{
    public string Name { get; set; }
    public string Description { get; set; }
    
    public CheckupResultForm Execute();
}