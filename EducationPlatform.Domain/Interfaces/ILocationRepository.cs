using EducationPlatform.Domain.Entities;

namespace EducationPlatform.Domain.Interfaces;

public interface ILocationRepository : IBaseRepository<LocationsEntity>
{
    Task<LocationsEntity?> GetByNameAsync(string name, CancellationToken cancellationToken);
}
