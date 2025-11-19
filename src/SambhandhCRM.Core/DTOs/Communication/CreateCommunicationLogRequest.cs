using SambhandhCRM.Core.Models.Enums;

namespace SambhandhCRM.Core.DTOs.Communication;

public class CreateCommunicationLogRequest
{
    public long? CustomerId { get; set; }
    public long? LeadId { get; set; }
    public DateTime CommunicationDate { get; set; } = DateTime.UtcNow;
    public CommunicationType CommunicationType { get; set; }
    public CommunicationDirection Direction { get; set; }
    public string Subject { get; set; } = string.Empty;
    public string? Summary { get; set; }
    public string? NextActionRequired { get; set; }
}
