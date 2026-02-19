
// I learned that only Lists and other "ReadOnly" methods should have "AsNoTracking()". I was having problems with my database not updating...

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
            .Include(l => l.Lessons)
            .FirstOrDefaultAsync(i => i.Email == email, cancellationToken);
    }

    public async Task<InstructorsEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _table
            .Include(l => l.Lessons)
            .FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
    }
}
