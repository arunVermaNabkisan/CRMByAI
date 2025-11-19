using SambhandhCRM.Core.Models.Enums;

namespace SambhandhCRM.Core.Models;

/// <summary>
/// Tracks the history of status changes for leads
/// </summary>
public class LeadStatusHistory : BaseEntity
{
    public Guid LeadId { get; set; }
    public Lead Lead { get; set; } = null!;

    public LeadStatus FromStatus { get; set; }
    public LeadStatus ToStatus { get; set; }
    public string? Remarks { get; set; }
    public DateTime ChangedAt { get; set; } = DateTime.UtcNow;
    public string ChangedBy { get; set; } = string.Empty;
}
