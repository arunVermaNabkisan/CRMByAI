using SambhandhCRM.Core.Models.Enums;

namespace SambhandhCRM.Core.DTOs.Customer;

public class CreateCustomerRequest
{
    // Basic Information
    public LegalConstitution LegalConstitution { get; set; }
    public string EntityName { get; set; } = string.Empty;
    public string? RegistrationNumber { get; set; }
    public string? CIN { get; set; }
    public string? PANNumber { get; set; }
    public string? AadhaarNumber { get; set; }
    public DateTime? DateOfIncorporation { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? PrimaryBusinessActivity { get; set; }
    public string? Occupation { get; set; }
    public decimal? AnnualTurnover { get; set; }
    public string? EmployeeCountRange { get; set; }
    public string? AnnualIncomeRange { get; set; }

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

    // Business Segments
    public List<Guid> BusinessSegmentIds { get; set; } = new();
}
