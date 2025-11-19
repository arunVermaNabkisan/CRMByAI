using SambhandhCRM.Core.Models;
using SambhandhCRM.Core.Models.Enums;

namespace SambhandhCRM.Core.Repositories.Interfaces;

public interface ICustomerRepository
{
    Task<IEnumerable<Customer>> GetAllAsync();
    Task<Customer?> GetByIdAsync(long id);
    Task<Customer?> GetByIdWithDetailsAsync(long id);
    Task<IEnumerable<Customer>> SearchAsync(string searchTerm);
    Task<IEnumerable<Customer>> GetByStatusAsync(CustomerStatus status);
    Task<long> CreateAsync(Customer customer);
    Task<bool> UpdateAsync(Customer customer);
    Task<bool> DeleteAsync(long id);
    Task<bool> ExistsByPANAsync(string panNumber);
}
