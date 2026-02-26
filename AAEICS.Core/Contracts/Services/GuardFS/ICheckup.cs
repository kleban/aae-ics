using AAEICS.Core.DTO.GuardFS;

namespace AAEICS.Core.Contracts.Services.GuardFS;

public interface ICheckup
{
    public string Name { get; set; }
    public string Description { get; set; }    
    public CheckupResultDTO Execute();
}