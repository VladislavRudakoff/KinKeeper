namespace KinKeeper.Core.Models;

public sealed class UserProfile(Guid id)
{
    public Guid Id { get; } = id;

    public required string FullName { get; init; }

    public DateTimeOffset? Birthday { get; init; }
}
