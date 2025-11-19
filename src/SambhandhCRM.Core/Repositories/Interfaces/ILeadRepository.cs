using SambhandhCRM.Core.Models;
using SambhandhCRM.Core.Models.Enums;

namespace SambhandhCRM.Core.Repositories.Interfaces;

public interface ILeadRepository
{
    Task<IEnumerable<Lead>> GetAllAsync();
    Task<Lead?> GetByIdAsync(Guid id);
    Task<Lead?> GetByIdWithDetailsAsync(Guid id);
    Task<IEnumerable<Lead>> GetByCustomerIdAsync(Guid customerId);
    Task<IEnumerable<Lead>> GetByStatusAsync(LeadStatus status);
    Task<IEnumerable<Lead>> GetByAssignedUserAsync(string userId);
    Task<Guid> CreateAsync(Lead lead);
    Task<bool> UpdateAsync(Lead lead);
    Task<bool> UpdateStatusAsync(Guid id, LeadStatus status);
    Task<bool> DeleteAsync(Guid id);
    Task<string> GenerateLeadNumberAsync();
}
