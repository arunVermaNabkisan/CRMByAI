using SambhandhCRM.Core.Models.Enums;

namespace SambhandhCRM.Core.DTOs.Lead;

public class CreateLeadRequest
{
    public Guid CustomerId { get; set; }
    public string LeadSource { get; set; } = string.Empty;
    public string? ReferralSource { get; set; }
    public string ProductInterest { get; set; } = string.Empty;
    public string? LoanAmountRange { get; set; }
    public Priority Priority { get; set; } = Priority.Medium;
    public string AssignedToUserId { get; set; } = string.Empty;
    public DateTime? NextFollowUpDate { get; set; }
    public string? Notes { get; set; }
}
