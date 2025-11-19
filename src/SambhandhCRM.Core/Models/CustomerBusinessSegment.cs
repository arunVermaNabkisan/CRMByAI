namespace SambhandhCRM.Core.Models;

/// <summary>
/// Junction table for Customer and BusinessSegment (many-to-many)
/// </summary>
public class CustomerBusinessSegment
{
    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;

    public Guid BusinessSegmentId { get; set; }
    public BusinessSegment BusinessSegment { get; set; } = null!;

    public bool IsPrimary { get; set; } = false;
    public DateTime AssignedAt { get; set; } = DateTime.UtcNow;
}
