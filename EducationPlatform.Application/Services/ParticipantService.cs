
// ChatGPT has helped me explain how to retrieve a specific phone number for a Participant in order to delete exactly that phone number without deleting all phone numbers.
// See comments in the code.

using EducationPlatform.Application.Abstractions.Persistence;
using EducationPlatform.Application.DTOs.Participants;
using EducationPlatform.Application.DTOs.Phonenumbers;
using EducationPlatform.Application.Mappers.Participants;
using EducationPlatform.Application.Mappers.Phonenumbers;
using EducationPlatform.Application.ServiceInterfaces;
using EducationPlatform.Domain.Interfaces;

namespace EducationPlatform.Application.Services;

public class ParticipantService : IParticipantService
{
    private readonly IParticipantRepository _participantRepository;
    private readonly IPhonenumberRepository _phonenumberRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ParticipantService(IParticipantRepository ParticipantRepository, IPhonenumberRepository PhonenumberRepository, IUnitOfWork unitOfWork)
    {
        _participantRepository = ParticipantRepository;
        _phonenumberRepository = PhonenumberRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ParticipantResponseDTO> CreateParticipantAsync(CreateParticipantDTO participantDTO, CancellationToken cancellationToken)
    {
        var savedParticipant = ParticipantMapper.ToEntity(participantDTO);
        await _participantRepository.CreateAsync(savedParticipant, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return ParticipantMapper.ToDTO(savedParticipant);
    }

    public async Task<ParticipantResponseDTO> GetParticipantByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var participant = await _participantRepository.GetByIdAsync(id, cancellationToken);
        if (participant == null) 
            throw new ArgumentNullException(nameof(participant));

        return ParticipantMapper.ToDTO(participant);
    }

    public async Task<IReadOnlyList<ParticipantResponseDTO>> GetAllParticipantsAsync(CancellationToken cancellationToken)
    {   
        var participants = await _participantRepository.GetAllAsync(cancellationToken);
        return participants.Select(ParticipantMapper.ToDTO).ToList();
    }

    public async Task<ParticipantResponseDTO> UpdateParticipantAsync(UpdateParticipantDTO participantDTO, CancellationToken cancellationToken)
    {
        if(participantDTO == null)  
            throw new ArgumentNullException(nameof(participantDTO));

        var participantToUpdate = await _participantRepository.GetByEmailAsync(participantDTO.Email!, cancellationToken);
        if(participantToUpdate == null)
            throw new ArgumentNullException(nameof(participantToUpdate));

        ParticipantMapper.UpdateEntity(participantToUpdate, participantDTO);
        await _unitOfWork.CommitAsync(cancellationToken);

        return ParticipantMapper.ToDTO(participantToUpdate);
    }

    public async Task DeleteParticipantAsync(string participantName, CancellationToken cancellationToken)
    {
        var participantToDelte = await _participantRepository.GetByEmailAsync(participantName, cancellationToken);
        if(participantName == null)
            throw new ArgumentNullException(nameof(participantName));

        await _participantRepository.DeleteAsync(participantToDelte!, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);
    }




    public async Task<PhonenumberResponseDTO> CreatePhonenumberToParticipantAsync(Guid participantId, CreatePhonenumberDTO phonenumberDTO, CancellationToken cancellationToken)
    {
        var savedPhonenumber = PhonenumberMapper.ToEntity(phonenumberDTO);
        await _phonenumberRepository.CreateAsync(savedPhonenumber, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return PhonenumberMapper.ToDTO(savedPhonenumber);
    }

    // -------------------------- See note! -----------------------------

    public async Task DeletePhonenumberFromParticipantAsync(Guid participantId, Guid phonenumberId, CancellationToken cancellationToken)
    {
        var phonenumbers = await _phonenumberRepository.GetByParticipantAsync(participantId, cancellationToken); // Get all phonenumber for a participant.
        var phonenumberToDelete = phonenumbers.FirstOrDefault(p => p.Id == phonenumberId); // Find phonenumber to delete.
        if (phonenumberToDelete == null)
            throw new ArgumentNullException(nameof(phonenumberToDelete));

        await _phonenumberRepository.DeleteAsync(phonenumberToDelete, cancellationToken);
    }
}
