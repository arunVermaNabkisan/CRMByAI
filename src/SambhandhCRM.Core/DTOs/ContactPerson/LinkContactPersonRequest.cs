namespace SambhandhCRM.Core.DTOs.ContactPerson;

public class LinkContactPersonRequest
{
    public long CustomerId { get; set; }
    public long ContactPersonId { get; set; }
    public string RoleInOrganization { get; set; } = string.Empty;
    public bool IsDecisionMaker { get; set; } = false;
    public bool IsPreferredContact { get; set; } = false;
    public string? CustomRoleDescription { get; set; }
}
