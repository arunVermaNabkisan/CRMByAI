using SambhandhCRM.Core.Models;

namespace SambhandhCRM.Core.Repositories.Interfaces;

public interface ICommunicationLogRepository
{
    Task<IEnumerable<CommunicationLog>> GetAllAsync();
    Task<CommunicationLog?> GetByIdAsync(long id);
    Task<IEnumerable<CommunicationLog>> GetByCustomerIdAsync(long customerId);
    Task<IEnumerable<CommunicationLog>> GetByLeadIdAsync(long leadId);
    Task<long> CreateAsync(CommunicationLog communicationLog);
    Task<bool> UpdateAsync(CommunicationLog communicationLog);
    Task<bool> DeleteAsync(long id);
}
