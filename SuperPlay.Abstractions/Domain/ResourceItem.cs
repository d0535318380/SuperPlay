using MessagePack;

namespace SuperPlay.Abstractions.Domain;

[MessagePackObject(keyAsPropertyName: true)]
public class ResourceItem
{
    public ResourceTypeEnum Type { get; set; } = ResourceTypeEnum.Wallet;
    public int Key { get; set; }
    public int Value { get; set; }
}