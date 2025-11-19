using SambhandhCRM.Core.Models.Enums;

namespace SambhandhCRM.Core.DTOs.Communication;

public class CommunicationLogResponse
{
    public Guid Id { get; set; }
    public Guid? CustomerId { get; set; }
    public string? CustomerName { get; set; }
    public Guid? LeadId { get; set; }
    public string? LeadNumber { get; set; }
    public DateTime CommunicationDate { get; set; }
    public CommunicationType CommunicationType { get; set; }
    public CommunicationDirection Direction { get; set; }
    public string Subject { get; set; } = string.Empty;
    public string? Summary { get; set; }
    public string? NextActionRequired { get; set; }
    public string LoggedByUserName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
