namespace SambhandhCRM.Core.DTOs.MasterData;

public class MasterDataResponse
{
    public long Id { get; set; }
    public string Category { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsSystemDefined { get; set; }
}
