using SambhandhCRM.Core.Models;

namespace SambhandhCRM.Core.Repositories.Interfaces;

public interface ICommunicationLogRepository
{
    Task<IEnumerable<CommunicationLog>> GetAllAsync();
    Task<CommunicationLog?> GetByIdAsync(Guid id);
    Task<IEnumerable<CommunicationLog>> GetByCustomerIdAsync(Guid customerId);
    Task<IEnumerable<CommunicationLog>> GetByLeadIdAsync(Guid leadId);
    Task<Guid> CreateAsync(CommunicationLog communicationLog);
    Task<bool> UpdateAsync(CommunicationLog communicationLog);
    Task<bool> DeleteAsync(Guid id);
}
