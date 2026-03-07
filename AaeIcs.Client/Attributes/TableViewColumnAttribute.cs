namespace AAEICS.Client.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class GridColumnAttribute(string width = "Auto", string stringFormat = null) : Attribute
{
    public string Width { get; set; } = width; 
    public string StringFormat { get; set; } = stringFormat;
}
