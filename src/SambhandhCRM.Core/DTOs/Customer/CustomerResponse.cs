using SambhandhCRM.Core.Models.Enums;

namespace SambhandhCRM.Core.DTOs.Customer;

public class CustomerResponse
{
    public long Id { get; set; }
    public LegalConstitution LegalConstitution { get; set; }
    public string EntityName { get; set; } = string.Empty;
    public string? PANNumber { get; set; }
    public string? CIN { get; set; }
    public CustomerStatus Status { get; set; }
    public string PrimaryPhone { get; set; } = string.Empty;
    public string? PrimaryEmail { get; set; }
    public List<string> BusinessSegments { get; set; } = new();
    public DateTime CreatedAt { get; set; }
    public string? AssignedToUserName { get; set; }
}
