namespace SambhandhCRM.Core.Models;

/// <summary>
/// Relationship mapping between customers and contact persons
/// </summary>
public class CustomerContactPerson
{
    public long CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;

    public long ContactPersonId { get; set; }
    public ContactPerson ContactPerson { get; set; } = null!

    public string RoleInOrganization { get; set; } = string.Empty;
    public DateTime RoleStartDate { get; set; } = DateTime.UtcNow;
    public bool IsStillActive { get; set; } = true;
    public bool IsDecisionMaker { get; set; } = false;
    public bool IsPreferredContact { get; set; } = false;
    public string? CustomRoleDescription { get; set; }
}
