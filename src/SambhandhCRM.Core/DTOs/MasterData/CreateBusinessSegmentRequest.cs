namespace SambhandhCRM.Core.DTOs.MasterData;

public class CreateBusinessSegmentRequest
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int DisplayOrder { get; set; }
}
