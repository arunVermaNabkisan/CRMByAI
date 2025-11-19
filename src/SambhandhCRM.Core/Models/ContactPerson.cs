namespace SambhandhCRM.Core.Models;

/// <summary>
/// Individual profiles for key personnel in customer organizations
/// </summary>
public class ContactPerson : BaseEntity
{
    public string FullName { get; set; } = string.Empty;
    public string MobileNumber { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? PANNumber { get; set; }
    public string? DIN { get; set; } // Director Identification Number
    public string? LinkedInProfile { get; set; }

    // Navigation Properties
    public ICollection<CustomerContactPerson> CustomerRelationships { get; set; } = new List<CustomerContactPerson>();
}
