using SambhandhCRM.Core.Models;

namespace SambhandhCRM.Core.Repositories.Interfaces;

public interface IContactPersonRepository
{
    Task<IEnumerable<ContactPerson>> GetAllAsync();
    Task<ContactPerson?> GetByIdAsync(long id);
    Task<IEnumerable<ContactPerson>> GetByCustomerIdAsync(long customerId);
    Task<long> CreateAsync(ContactPerson contactPerson);
    Task<bool> UpdateAsync(ContactPerson contactPerson);
    Task<bool> DeleteAsync(long id);
    Task<bool> LinkToCustomerAsync(long contactPersonId, long customerId, bool isPrimary = false);
}
