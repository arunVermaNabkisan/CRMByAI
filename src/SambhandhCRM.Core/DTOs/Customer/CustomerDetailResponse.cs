using SambhandhCRM.Core.Models.Enums;

namespace SambhandhCRM.Core.DTOs.Customer;

public class CustomerDetailResponse : CustomerResponse
{
    // All fields from CreateCustomerRequest
    public string? RegistrationNumber { get; set; }
    public string? AadhaarNumber { get; set; }
    public DateTime? DateOfIncorporation { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? PrimaryBusinessActivity { get; set; }
    public string? Occupation { get; set; }
    public decimal? AnnualTurnover { get; set; }
    public string? EmployeeCountRange { get; set; }
    public string? AnnualIncomeRange { get; set; }

    public string RegisteredAddress { get; set; } = string.Empty;
    public string? OfficeAddress { get; set; }
    public string? CorrespondenceAddress { get; set; }
    public string RegisteredPinCode { get; set; } = string.Empty;
    public string? OfficePinCode { get; set; }
    public string? CorrespondencePinCode { get; set; }
    public string? MobileNumber { get; set; }
    public string? AlternativePhone { get; set; }
    public string? SecondaryEmail { get; set; }
    public string? Website { get; set; }
    public string? LinkedInProfile { get; set; }
    public string? TwitterProfile { get; set; }

    public string? PrimaryBankName { get; set; }
    public int? BankingSinceYear { get; set; }
    public bool IsExistingNABKISANCustomer { get; set; }
    public string? ExistingProductType { get; set; }
    public decimal? OutstandingAmount { get; set; }
    public string? OtherLenderRelationships { get; set; }

    public string? AuthorizedCapital { get; set; }
    public string? PaidUpCapital { get; set; }
    public DateTime? MCALastFetchedAt { get; set; }
}
