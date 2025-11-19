using SambhandhCRM.Core.Models.Enums;

namespace SambhandhCRM.Core.DTOs.Lead;

public class UpdateLeadStatusRequest
{
    public Guid LeadId { get; set; }
    public LeadStatus NewStatus { get; set; }
    public string? Remarks { get; set; }
    public string? DropReason { get; set; } // Required when status is Dropped
}
