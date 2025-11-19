namespace SambhandhCRM.Core.Models;

/// <summary>
/// Master data for business segments (FPO, Agri-Startup, MFI, etc.)
/// </summary>
public class BusinessSegment : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;
    public int DisplayOrder { get; set; }

    // Navigation Properties
    public ICollection<CustomerBusinessSegment> CustomerBusinessSegments { get; set; } = new List<CustomerBusinessSegment>();
}
