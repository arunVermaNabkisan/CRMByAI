namespace SambhandhCRM.Core.Models;

/// <summary>
/// Individual profiles for key personnel in customer organizations
/// Maps to Individual table in SSDT database
/// </summary>
public class Individual : BaseEntity
{
    public long IndividualId { get { return Id; } set { Id = value; } }

    // Personal Details
    public string FullName { get; set; } = string.Empty;
    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string? LastName { get; set; }
    public string? Gender { get; set; } // Male, Female, Other
    public DateTime? DateOfBirth { get; set; }

    // Contact Details
    public string? MobileNumber { get; set; }
    public string? AlternateMobileNumber { get; set; }
    public string? EmailAddress { get; set; }
    public string? AlternateEmailAddress { get; set; }

    // Identification
    public string? PANNumber { get; set; }
    public string? AadhaarNumber { get; set; }
    public string? DINNumber { get; set; }
    public string? PassportNumber { get; set; }

    // Professional Details
    public string? Designation { get; set; }
    public string? Department { get; set; }
    public string? Qualification { get; set; }
    public int? Experience { get; set; } // Years of experience

    // Social Media
    public string? LinkedInProfile { get; set; }
    public string? TwitterHandle { get; set; }

    // Address (Primary)
    public string? AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? PINCode { get; set; }

    // Flags
    public bool IsKeyDecisionMaker { get; set; }
    public bool IsVerified { get; set; }
    public long? VerifiedBy { get; set; }
    public DateTime? VerifiedDate { get; set; }

    // Navigation Properties
    public ICollection<PartyIndividualRelationship> PartyRelationships { get; set; } = new List<PartyIndividualRelationship>();
}
