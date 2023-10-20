using System.ComponentModel.DataAnnotations;

public class User : IHasId<Guid>
{
    [Required]
    public Guid Id { get; set; }
    
    [Required, MaxLength(50)]
    public string Title { get; set; }


    public ICollection<UserResource> Resources { get; set; } = new List<UserResource>();
    public ICollection<UserToken> Tokens { get; set; } = new List<UserToken>();
}