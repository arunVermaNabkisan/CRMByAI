using SambhandhCRM.Core.Models.Enums;

namespace SambhandhCRM.Core.DTOs.Customer;

public class UpdateCustomerRequest : CreateCustomerRequest
{
    public Guid Id { get; set; }
    public CustomerStatus Status { get; set; }
}
