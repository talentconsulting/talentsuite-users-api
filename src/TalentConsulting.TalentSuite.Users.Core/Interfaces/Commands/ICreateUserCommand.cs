using TalentConsulting.TalentSuite.Users.Common.Entities;

namespace TalentConsulting.TalentSuite.Reports.Core.Interfaces.Commands;

public interface ICreateUserCommand
{
    UserDto UserDto { get; }
}
