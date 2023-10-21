using System.ComponentModel.DataAnnotations;
using SuperPlay.Abstractions.Domain;

public class User : IHasId<Guid>
{
    [Required]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Required, MaxLength(50)]
    public string Title { get; set; }


    public ICollection<UserResource> Resources { get; set; } = new List<UserResource>();
    public ICollection<UserToken> Tokens { get; set; } = new List<UserToken>();


    public static User Create(string title, string? token = default, Guid? id = default)
    {
        var instance = new User
        {
            Id = id ?? Guid.NewGuid(),
            Title = title,
        };
        
        instance.Resources.Add(UserResource.Wallet(instance.Id, WalletTypeEnum.Coins));
        instance.Resources.Add(UserResource.Wallet(instance.Id, WalletTypeEnum.Rolls));
        instance.Tokens.Add(UserToken.Create(instance.Id, token ?? Guid.NewGuid().ToString()));
        
        return instance;
    } 
}