using TalentConsulting.TalentSuite.Users.Common.Interfaces;

namespace TalentConsulting.TalentSuite.Users.Infrastructure.Service;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.UtcNow;
}

