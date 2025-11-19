using SambhandhCRM.Core.Models.Enums;

namespace SambhandhCRM.Core.DTOs.Lead;

public class LeadResponse
{
    public Guid Id { get; set; }
    public string LeadNumber { get; set; } = string.Empty;
    public Guid CustomerId { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string LeadSource { get; set; } = string.Empty;
    public string ProductInterest { get; set; } = string.Empty;
    public string? LoanAmountRange { get; set; }
    public Priority Priority { get; set; }
    public LeadStatus Status { get; set; }
    public string AssignedToUserName { get; set; } = string.Empty;
    public DateTime? LastContactDate { get; set; }
    public DateTime? NextFollowUpDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public int DaysInPipeline { get; set; }
}
