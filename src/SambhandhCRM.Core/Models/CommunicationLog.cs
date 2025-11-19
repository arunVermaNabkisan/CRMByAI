namespace SambhandhCRM.Core.Models;

/// <summary>
/// Tracks all customer interactions and communications
/// Maps to CommunicationLog table in SSDT database
/// </summary>
public class CommunicationLog : BaseEntity
{
    public long CommunicationId { get { return Id; } set { Id = value; } }

    // Related Entities
    public long? PartyId { get; set; }
    public long? IndividualId { get; set; }
    public long? LeadId { get; set; }

    // Communication Details
    public string CommunicationType { get; set; } = string.Empty; // Phone Call, Email, Meeting, Site Visit, WhatsApp, SMS
    public string Direction { get; set; } = string.Empty; // Inbound, Outbound
    public DateTime CommunicationDate { get; set; } = DateTime.UtcNow;
    public int? Duration { get; set; } // In minutes

    // Contact Details
    public string? ContactPerson { get; set; }
    public string? ContactNumber { get; set; }
    public string? ContactEmail { get; set; }

    // Content
    public string? Subject { get; set; }
    public string? Summary { get; set; }
    public string? DetailedNotes { get; set; }

    // Outcome & Follow-up
    public string? Outcome { get; set; }
    public string? NextAction { get; set; }
    public DateTime? NextFollowUpDate { get; set; }

    // Campaign Details (for bulk communication)
    public long? CampaignId { get; set; }
    public long? TemplateId { get; set; }
    public bool IsBulkCommunication { get; set; }

    // Delivery Status (for SMS/Email)
    public string? DeliveryStatus { get; set; } // Sent, Delivered, Failed, Bounced, Opened, Clicked
    public DateTime? DeliveryStatusDate { get; set; }
    public string? DeliveryStatusDetails { get; set; }

    // Attachments
    public bool HasAttachments { get; set; }
    public int AttachmentCount { get; set; }

    // Navigation Properties
    public Customer? Party { get; set; }
    public Individual? Individual { get; set; }
    public Lead? Lead { get; set; }
}
