using System.ComponentModel.DataAnnotations;

public class UserToken : IHasId<int>
{
 
    [Required]
    public int Id { get; set; }

    [Required]
    public Guid UserId { get; set; }

    [Required, MaxLength(1000)]
    public string Token { get; set; }

    public User? User { get; set; }

    public static UserToken Create(Guid instanceId, string token)
    => new()   {
        UserId = instanceId,
        Token = token
    };
}