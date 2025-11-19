namespace SambhandhCRM.Core.Models;

/// <summary>
/// Lead tracking for potential business opportunities
/// Maps to Lead table in SSDT database
/// </summary>
public class Lead : BaseEntity
{
    public long LeadId { get { return Id; } set { Id = value; } }
    public string LeadNumber { get; set; } = string.Empty; // LEAD-YYYYMM-9999
    public long PartyId { get; set; }

    // Lead Source & Classification
    public string LeadSource { get; set; } = string.Empty; // Direct Walk-in, Website, Referral, Campaign, Others
    public string? LeadSourceOther { get; set; }
    public string? ReferralSource { get; set; } // Name of the referrer
    public long? ReferralPartyId { get; set; }
    public long? ReferralEmployeeId { get; set; }
    public long? CampaignId { get; set; }

    // Product & Amount
    public string ProductInterest { get; set; } = string.Empty; // Term Loan, Working Capital, Guarantee, Others
    public string? ProductInterestOther { get; set; }
    public string? LoanAmountRange { get; set; }
    public decimal? EstimatedLoanAmount { get; set; }
    public int? RequiredTenureMonths { get; set; }

    // Priority & Status
    public string Priority { get; set; } = "Medium"; // High, Medium, Low
    public string Status { get; set; } = "New"; // New, In-Progress, Documentation, Submitted to Credit, Dropped, Converted
    public string? SubStatus { get; set; }

    // Assignment
    public long AssignedToUserId { get; set; }
    public long? RegionalManagerId { get; set; }
    public DateTime AssignedDate { get; set; }

    // Follow-up & Activity
    public DateTime? LastContactDate { get; set; }
    public DateTime? NextFollowUpDate { get; set; }
    public DateTime? ExpectedClosureDate { get; set; }

    // Conversion
    public bool IsConverted { get; set; }
    public DateTime? ConvertedDate { get; set; }
    public decimal? ConversionValue { get; set; }

    // Dropped Details
    public bool IsDropped { get; set; }
    public DateTime? DroppedDate { get; set; }
    public string? DropReason { get; set; }
    public string? DropReasonCategory { get; set; }

    // Notes
    public string? Notes { get; set; }
    public string? InternalNotes { get; set; }

    // Document Checklist
    public bool IsKYCReceived { get; set; }
    public bool IsFinancialStatementsReceived { get; set; }
    public bool IsBusinessDocumentsReceived { get; set; }
    public bool IsOtherDocumentsReceived { get; set; }

    // Aging - computed in database
    public int DaysInPipeline { get; set; }

    // Navigation Properties
    public Customer? Customer { get; set; }
    public Customer? ReferralParty { get; set; }
    public ICollection<CommunicationLog> CommunicationLogs { get; set; } = new List<CommunicationLog>();
}
