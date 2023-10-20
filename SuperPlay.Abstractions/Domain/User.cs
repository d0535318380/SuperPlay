using System.ComponentModel.DataAnnotations;

public class User : IHasId<Guid>
{
    [Required]
    public Guid Id { get; set; }
    
    [Required, MaxLength(50)]
    public string Title { get; set; }
}