using EducationPlatform.Domain.Entities;

namespace EducationPlatform.Domain.Interfaces;

public interface IInstructorRepository : IBaseRepository<InstructorsEntity>
{
    Task<InstructorsEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<InstructorsEntity?> GetByEmailAsync(string email, CancellationToken cancellationToken);
    Task<InstructorsEntity?> UpdateAsync(InstructorsEntity instructor, CancellationToken cancellationToken);
    Task DeleteAsync(InstructorsEntity instructor, CancellationToken cancellationToken);
}
