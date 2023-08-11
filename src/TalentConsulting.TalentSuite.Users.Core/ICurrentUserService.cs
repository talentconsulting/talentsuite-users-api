namespace TalentConsulting.TalentSuite.Users.Core;

public interface ICurrentUserService
{
    string? UserId { get; }
}
