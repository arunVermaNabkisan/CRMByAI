namespace SambhandhCRM.Core.DTOs.ContactPerson;

public class CreateContactPersonRequest
{
    public string FullName { get; set; } = string.Empty;
    public string MobileNumber { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? PANNumber { get; set; }
    public string? DIN { get; set; }
    public string? LinkedInProfile { get; set; }
}
