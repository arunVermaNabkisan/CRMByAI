using SambhandhCRM.Core.Models.Enums;

namespace SambhandhCRM.Core.Models;

/// <summary>
/// Lead tracking for potential business opportunities
/// </summary>
public class Lead : BaseEntity
{
    public string LeadNumber { get; set; } = string.Empty; // LEAD-YYYYMM-9999
    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;

    // Lead Details
    public string LeadSource { get; set; } = string.Empty;
    public string? ReferralSource { get; set; }
    public string ProductInterest { get; set; } = string.Empty;
    public string? LoanAmountRange { get; set; }
    public Priority Priority { get; set; } = Priority.Medium;
    public LeadStatus Status { get; set; } = LeadStatus.New;

    // Assignment
    public string AssignedToUserId { get; set; } = string.Empty;
    public DateTime? AssignedAt { get; set; }

    // Activity Tracking
    public DateTime? LastContactDate { get; set; }
    public DateTime? NextFollowUpDate { get; set; }
    public string? Notes { get; set; }

    // Document Checklist
    public bool HasKYCDocuments { get; set; }
    public bool HasFinancialStatements { get; set; }
    public bool HasBusinessDocuments { get; set; }
    public bool HasOtherDocuments { get; set; }

    // Conversion
    public DateTime? ConvertedAt { get; set; }
    public string? DropReason { get; set; }

    // Navigation Properties
    public ICollection<LeadStatusHistory> StatusHistory { get; set; } = new List<LeadStatusHistory>();
    public ICollection<CommunicationLog> CommunicationLogs { get; set; } = new List<CommunicationLog>();
}
