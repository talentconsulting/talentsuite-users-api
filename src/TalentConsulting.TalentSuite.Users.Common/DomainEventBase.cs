using MediatR;

namespace TalentConsulting.TalentSuite.Users.Common;

public abstract class DomainEventBase : INotification
{
    public DateTimeOffset DateOccurred { get; protected set; } = DateTimeOffset.UtcNow;
}

