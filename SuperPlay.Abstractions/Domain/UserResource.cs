using System.ComponentModel.DataAnnotations;
using SuperPlay.Abstractions.Domain;

public class UserResource : IHasId<string>
{
    [Required, MaxLength(100)]
    public string Id { get; set; }

    public Guid UserId { get; set; }
    
    [Required]
    public ResourceTypeEnum ResourceType { get; set; }

    [Required]
    public int Key { get; set; }
    
    [Required]
    public int Value { get; set; }

    public User? User { get; set; }
}