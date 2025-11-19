using SambhandhCRM.Core.Models.Enums;

namespace SambhandhCRM.Core.DTOs.Lead;

public class UpdateLeadRequest
{
    public Guid Id { get; set; }
    public string? LeadSource { get; set; }
    public string? ReferralSource { get; set; }
    public string? ProductInterest { get; set; }
    public string? LoanAmountRange { get; set; }
    public Priority? Priority { get; set; }
    public string? AssignedToUserId { get; set; }
    public DateTime? NextFollowUpDate { get; set; }
    public string? Notes { get; set; }

    // Document checklist
    public bool? HasKYCDocuments { get; set; }
    public bool? HasFinancialStatements { get; set; }
    public bool? HasBusinessDocuments { get; set; }
    public bool? HasOtherDocuments { get; set; }
}
