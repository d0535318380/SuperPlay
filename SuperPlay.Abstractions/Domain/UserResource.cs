using System.ComponentModel.DataAnnotations;
using SuperPlay.Abstractions.Domain;

public class UserResource : IHasId<int>
{
    [Required]
    public int Id { get; set; }

    [Required]
    public ResourceTypeEnum ResourceType { get; set; }

    [Required]
    public int ValueType { get; set; }
    
    [Required]
    public int Value { get; set; }
}