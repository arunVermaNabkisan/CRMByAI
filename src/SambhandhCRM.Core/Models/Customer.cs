namespace SambhandhCRM.Core.Models;

/// <summary>
/// Party Master - Central repository for all customer and prospect information
/// Maps to PartyMaster table in SSDT database
/// </summary>
public class Customer : BaseEntity
{
    public long PartyId { get { return Id; } set { Id = value; } }

    // Legal Constitution & Classification
    public string LegalConstitution { get; set; } = string.Empty; // Company, Society, Trust/NGO, Partnership/LLP, Individual/Proprietor
    public string? BusinessSegment { get; set; } // FPO, Agri-Startup, MFI, NBFC, HFC, CSR Partner, PACS, Others
    public string? BusinessSegmentOther { get; set; }

    // Organization Details (for non-individuals)
    public string? EntityName { get; set; }
    public string? RegistrationNumber { get; set; }
    public DateTime? DateOfIncorporation { get; set; }
    public string? PrimaryBusinessActivity { get; set; }
    public decimal? AnnualTurnover { get; set; }
    public string? EmployeeCountRange { get; set; }

    // Individual Details (for individuals/proprietors)
    public string? FullName { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? Occupation { get; set; }
    public string? AnnualIncomeRange { get; set; }

    // Common Fields
    public string? PANNumber { get; set; }
    public string? AadhaarNumber { get; set; }
    public string? GSTNumber { get; set; }

    // MCA Integration (for Companies)
    public string? CINNumber { get; set; }
    public bool IsMCAVerified { get; set; }
    public DateTime? MCAVerificationDate { get; set; }
    public string? MCAData { get; set; }

    // Address Information
    public string? RegisteredAddress { get; set; }
    public string? RegisteredPinCode { get; set; }
    public string? OfficeAddress { get; set; }
    public string? OfficePinCode { get; set; }
    public string? CorrespondenceAddress { get; set; }
    public string? CorrespondencePinCode { get; set; }

    // Contact Information
    public string? PrimaryPhone { get; set; }
    public string? MobileNumber { get; set; }
    public string? AlternativePhone { get; set; }
    public string? PrimaryEmail { get; set; }
    public string? SecondaryEmail { get; set; }

    // Website & Social Media
    public string? Website { get; set; }
    public string? LinkedInProfile { get; set; }
    public string? TwitterHandle { get; set; }

    // Customer Status & Classification
    public string Status { get; set; } = "Prospect"; // Prospect, Active Lead, Customer, Dormant, Archived
    public bool IsExistingCustomer { get; set; }
    public DateTime? CustomerSince { get; set; }
    public string? ExistingProductType { get; set; }
    public decimal? OutstandingAmount { get; set; }

    // Assignment & Ownership
    public long? AssignedToUserId { get; set; }
    public long? RegionalManagerId { get; set; }

    // Banking Information
    public string? PrimaryBankName { get; set; }
    public int? BankingSinceYear { get; set; }

    // Other Relationships
    public string? OtherLenders { get; set; } // JSON array of lender names
    public string? OtherLenderRelationships { get; set; }
    public bool IsExistingNABKISANCustomer { get; set; }

    // Data Quality
    public int? DataQualityScore { get; set; }
    public bool IsVerified { get; set; }
    public long? VerifiedBy { get; set; }
    public DateTime? VerifiedDate { get; set; }

    // Navigation Properties (loaded separately from related tables)
    public ICollection<Lead> Leads { get; set; } = new List<Lead>();
    public ICollection<Individual> ContactPersons { get; set; } = new List<Individual>();
    public ICollection<CommunicationLog> CommunicationLogs { get; set; } = new List<CommunicationLog>();

    // Computed helper property for display
    public string DisplayName => EntityName ?? FullName ?? "Unknown";
}
