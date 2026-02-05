using EducationPlatform.Domain.Entities;
using EducationPlatform.Domain.Interfaces;
using EducationPlatform.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EducationPlatform.Infrastructure.Repositories;

public class InstructorRepository(EducationPlatformDbContext context) : BaseRepository<InstructorsEntity>(context), IInstructorRepository
{
    public async Task<InstructorsEntity?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await _table
            .AsNoTracking()
            .Include(e => e.Expertises)
            .FirstOrDefaultAsync(i => i.Email == email, cancellationToken);
    }

    public async Task<InstructorsEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _table
            .AsNoTracking()
            .Include(e => e.Expertises)
            .FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
    }
}
