namespace SambhandhCRM.Core.Models;

/// <summary>
/// Relationship mapping between parties and individuals
/// Maps to PartyIndividualRelationship table in SSDT database
/// </summary>
public class PartyIndividualRelationship : BaseEntity
{
    public long RelationshipId { get { return Id; } set { Id = value; } }
    public long PartyId { get; set; }
    public long IndividualId { get; set; }

    // Relationship Details
    public string RoleInOrganization { get; set; } = string.Empty; // Chairman, CEO/MD, Director, CFO, etc.
    public string? RoleOther { get; set; }
    public string? Department { get; set; }

    // Dates
    public DateTime? RoleStartDate { get; set; }
    public DateTime? RoleEndDate { get; set; }
    public bool IsCurrentRole { get; set; } = true;

    // Flags
    public bool IsDecisionMaker { get; set; }
    public bool IsAuthorizedSignatory { get; set; }
    public bool IsPrimaryContact { get; set; }
    public bool IsPreferredContact { get; set; }

    // Contact Preferences
    public string? PreferredContactMethod { get; set; } // Phone, Email, WhatsApp, Meeting
    public string? PreferredContactTime { get; set; } // Morning, Afternoon, Evening

    // Notes
    public string? Notes { get; set; }

    // Navigation Properties
    public Customer? Party { get; set; }
    public Individual? Individual { get; set; }
}
