using EducationPlatform.Domain.Entities;
using EducationPlatform.Domain.Interfaces;
using EducationPlatform.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EducationPlatform.Infrastructure.Repositories;

public class LocationRepository(EducationPlatformDbContext context) : BaseRepository<LocationsEntity>(context), ILocationRepository
{
    public async Task<LocationsEntity?> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        return await _table
            .FirstOrDefaultAsync(l => l.Name == name, cancellationToken);
    }
}
