using SambhandhCRM.Core.Models;
using SambhandhCRM.Core.Models.Enums;

namespace SambhandhCRM.Core.Repositories.Interfaces;

public interface ICustomerRepository
{
    Task<IEnumerable<Customer>> GetAllAsync();
    Task<Customer?> GetByIdAsync(Guid id);
    Task<Customer?> GetByIdWithDetailsAsync(Guid id);
    Task<IEnumerable<Customer>> SearchAsync(string searchTerm);
    Task<IEnumerable<Customer>> GetByStatusAsync(CustomerStatus status);
    Task<Guid> CreateAsync(Customer customer);
    Task<bool> UpdateAsync(Customer customer);
    Task<bool> DeleteAsync(Guid id);
    Task<bool> ExistsByPANAsync(string panNumber);
}
