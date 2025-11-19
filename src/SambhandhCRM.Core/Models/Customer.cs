using SambhandhCRM.Core.Models.Enums;

namespace SambhandhCRM.Core.Models;

/// <summary>
/// Party Master - Central repository for all customer and prospect information
/// </summary>
public class Customer : BaseEntity
{
    // Basic Information
    public LegalConstitution LegalConstitution { get; set; }
    public string EntityName { get; set; } = string.Empty;
    public string? RegistrationNumber { get; set; }
    public string? CIN { get; set; }
    public string? PANNumber { get; set; }
    public string? AadhaarNumber { get; set; } // Masked for individuals
    public DateTime? DateOfIncorporation { get; set; }
    public DateTime? DateOfBirth { get; set; } // For individuals
    public string? PrimaryBusinessActivity { get; set; }
    public string? Occupation { get; set; } // For individuals
    public decimal? AnnualTurnover { get; set; }
    public string? EmployeeCountRange { get; set; }
    public string? AnnualIncomeRange { get; set; } // For individuals

    // Contact Information
    public string RegisteredAddress { get; set; } = string.Empty;
    public string? OfficeAddress { get; set; }
    public string? CorrespondenceAddress { get; set; }
    public string RegisteredPinCode { get; set; } = string.Empty;
    public string? OfficePinCode { get; set; }
    public string? CorrespondencePinCode { get; set; }
    public string PrimaryPhone { get; set; } = string.Empty;
    public string? MobileNumber { get; set; }
    public string? AlternativePhone { get; set; }
    public string? PrimaryEmail { get; set; }
    public string? SecondaryEmail { get; set; }
    public string? Website { get; set; }
    public string? LinkedInProfile { get; set; }
    public string? TwitterProfile { get; set; }

    // Banking & Financial
    public string? PrimaryBankName { get; set; }
    public int? BankingSinceYear { get; set; }
    public bool IsExistingNABKISANCustomer { get; set; }
    public string? ExistingProductType { get; set; }
    public decimal? OutstandingAmount { get; set; }
    public string? OtherLenderRelationships { get; set; }

    // Status and Classification
    public CustomerStatus Status { get; set; } = CustomerStatus.Prospect;
    public string? AssignedToUserId { get; set; }

    // MCA Integration Data
    public string? AuthorizedCapital { get; set; }
    public string? PaidUpCapital { get; set; }
    public DateTime? MCALastFetchedAt { get; set; }

    // Navigation Properties
    public ICollection<CustomerBusinessSegment> BusinessSegments { get; set; } = new List<CustomerBusinessSegment>();
    public ICollection<Lead> Leads { get; set; } = new List<Lead>();
    public ICollection<ContactPerson> ContactPersons { get; set; } = new List<ContactPerson>();
    public ICollection<CommunicationLog> CommunicationLogs { get; set; } = new List<CommunicationLog>();
}
