namespace SambhandhCRM.Core.DTOs.ContactPerson;

public class LinkContactPersonRequest
{
    public Guid CustomerId { get; set; }
    public Guid ContactPersonId { get; set; }
    public string RoleInOrganization { get; set; } = string.Empty;
    public bool IsDecisionMaker { get; set; } = false;
    public bool IsPreferredContact { get; set; } = false;
    public string? CustomRoleDescription { get; set; }
}
