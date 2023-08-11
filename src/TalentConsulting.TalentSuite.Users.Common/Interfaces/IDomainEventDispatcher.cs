namespace TalentConsulting.TalentSuite.Users.Common.Interfaces;

public interface IDomainEventDispatcher
{
    Task DispatchAndClearEvents(IEnumerable<EntityBase<string>> entitiesWithEvents);
}
