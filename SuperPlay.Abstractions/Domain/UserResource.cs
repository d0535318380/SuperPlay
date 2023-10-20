using System.ComponentModel.DataAnnotations;
using HashidsNet;
using SuperPlay.Abstractions.Domain;

public class UserResource : IHasId<int>
{
    private static Hashids _hashids = new(nameof(UserResource), 10);
   
    [Key]
    [Required, MaxLength(100)]
    public int Id { get; set; }

    public Guid UserId { get; set; }
    
    [Required]
    public ResourceTypeEnum ResourceType { get; set; }

    [Required]
    public int Key { get; set; }
    
    [Required]
    public int Value { get; set; }

    public User? User { get; set; }


    public static UserResource Wallet(Guid userId, WalletTypeEnum key, int val = 100)
        => new UserResource
        {
            UserId = userId,
            ResourceType = ResourceTypeEnum.Wallet,
            Key = (int)key,
            Value = val
        };
}