namespace SambhandhCRM.Core.DTOs.ContactPerson;

public class ContactPersonResponse
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string MobileNumber { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? PANNumber { get; set; }
    public string? DIN { get; set; }
    public string? LinkedInProfile { get; set; }
    public List<CustomerRelationshipDto> CustomerRelationships { get; set; } = new();
}

public class CustomerRelationshipDto
{
    public Guid CustomerId { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string RoleInOrganization { get; set; } = string.Empty;
    public bool IsDecisionMaker { get; set; }
    public bool IsPreferredContact { get; set; }
    public bool IsStillActive { get; set; }
}
