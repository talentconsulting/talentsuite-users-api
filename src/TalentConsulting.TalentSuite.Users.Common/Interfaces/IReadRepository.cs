using Ardalis.Specification;

namespace TalentConsulting.TalentSuite.Users.Common.Interfaces;


public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class, IAggregateRoot
{
}
