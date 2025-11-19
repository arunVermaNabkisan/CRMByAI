using SambhandhCRM.Core.Models;
using SambhandhCRM.Core.Models.Enums;

namespace SambhandhCRM.Core.Repositories.Interfaces;

public interface ILeadRepository
{
    Task<IEnumerable<Lead>> GetAllAsync();
    Task<Lead?> GetByIdAsync(long id);
    Task<Lead?> GetByIdWithDetailsAsync(long id);
    Task<IEnumerable<Lead>> GetByCustomerIdAsync(long customerId);
    Task<IEnumerable<Lead>> GetByStatusAsync(LeadStatus status);
    Task<IEnumerable<Lead>> GetByAssignedUserAsync(string userId);
    Task<long> CreateAsync(Lead lead);
    Task<bool> UpdateAsync(Lead lead);
    Task<bool> UpdateStatusAsync(long id, LeadStatus status);
    Task<bool> DeleteAsync(long id);
    Task<string> GenerateLeadNumberAsync();
}
