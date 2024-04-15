namespace Allspark.Domain.Entities;

public class Assignment : BaseEntity
{
    public string Code { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool Deleted { get; set; }
}
