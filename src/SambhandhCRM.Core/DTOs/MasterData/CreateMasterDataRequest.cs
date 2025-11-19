namespace SambhandhCRM.Core.DTOs.MasterData;

public class CreateMasterDataRequest
{
    public string Category { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int DisplayOrder { get; set; }
}
