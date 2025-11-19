namespace SambhandhCRM.Core.Models;

/// <summary>
/// Generic master data for configurable dropdowns (Lead Sources, Document Types, etc.)
/// </summary>
public class MasterData : BaseEntity
{
    public string Category { get; set; } = string.Empty; // LeadSource, ProductCategory, DocumentType, etc.
    public string Value { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;
    public int DisplayOrder { get; set; }
    public bool IsSystemDefined { get; set; } = false; // Cannot be deleted if true
}
