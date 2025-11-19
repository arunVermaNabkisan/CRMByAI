namespace SambhandhCRM.Core.Models;

/// <summary>
/// System users (Relationship Managers, Regional Managers, etc.)
/// </summary>
public class User : BaseEntity
{
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string? MobileNumber { get; set; }
    public string Role { get; set; } = string.Empty; // RM, Regional Manager, Senior Management, Admin
    public bool IsActive { get; set; } = true;
    public DateTime? LastLoginAt { get; set; }

    // Hierarchy
    public Guid? ReportsToUserId { get; set; }
    public User? ReportsTo { get; set; }
    public ICollection<User> Subordinates { get; set; } = new List<User>();
}
