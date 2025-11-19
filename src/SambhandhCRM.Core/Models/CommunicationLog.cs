using SambhandhCRM.Core.Models.Enums;

namespace SambhandhCRM.Core.Models;

/// <summary>
/// Tracks all customer interactions and communications
/// </summary>
public class CommunicationLog : BaseEntity
{
    public Guid? CustomerId { get; set; }
    public Customer? Customer { get; set; }

    public Guid? LeadId { get; set; }
    public Lead? Lead { get; set; }

    public DateTime CommunicationDate { get; set; } = DateTime.UtcNow;
    public CommunicationType CommunicationType { get; set; }
    public CommunicationDirection Direction { get; set; }
    public string Subject { get; set; } = string.Empty;
    public string? Summary { get; set; }
    public string? NextActionRequired { get; set; }
    public string LoggedByUserId { get; set; } = string.Empty;

    // For bulk communications
    public bool IsBulkCommunication { get; set; } = false;
    public Guid? CampaignId { get; set; }
    public string? DeliveryStatus { get; set; }
}
