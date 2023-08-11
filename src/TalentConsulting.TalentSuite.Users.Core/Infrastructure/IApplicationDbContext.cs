using Microsoft.EntityFrameworkCore;

namespace TalentConsulting.TalentSuite.Users.Core.Infrastructure;

public interface IApplicationDbContext
{    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
