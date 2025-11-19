using SambhandhCRM.Core.Models;

namespace SambhandhCRM.Core.Repositories.Interfaces;

public interface IContactPersonRepository
{
    Task<IEnumerable<ContactPerson>> GetAllAsync();
    Task<ContactPerson?> GetByIdAsync(Guid id);
    Task<IEnumerable<ContactPerson>> GetByCustomerIdAsync(Guid customerId);
    Task<Guid> CreateAsync(ContactPerson contactPerson);
    Task<bool> UpdateAsync(ContactPerson contactPerson);
    Task<bool> DeleteAsync(Guid id);
    Task<bool> LinkToCustomerAsync(Guid contactPersonId, Guid customerId, bool isPrimary = false);
}
