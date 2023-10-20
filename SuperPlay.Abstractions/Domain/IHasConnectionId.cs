namespace SuperPlay.Abstractions.Domain;

public interface IHasConnectionId
{
    public string? ConnectionId { get; set; }
}