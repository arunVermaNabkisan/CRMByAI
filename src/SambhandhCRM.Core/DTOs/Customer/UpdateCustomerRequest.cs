using SambhandhCRM.Core.Models.Enums;

namespace SambhandhCRM.Core.DTOs.Customer;

public class UpdateCustomerRequest : CreateCustomerRequest
{
    public long Id { get; set; }
    public CustomerStatus Status { get; set; }
}
