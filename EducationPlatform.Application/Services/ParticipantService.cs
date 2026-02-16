
// ChatGPT has helped me explain how to retrieve a specific phone number for a Participant in order to delete exactly that phone number without deleting all phone numbers.
// See comments in the code.

using EducationPlatform.Application.Abstractions.Persistence;
using EducationPlatform.Application.DTOs.Participants;
using EducationPlatform.Application.DTOs.Phonenumbers;
using EducationPlatform.Application.Mappers.Participants;
using EducationPlatform.Application.Mappers.Phonenumbers;
using EducationPlatform.Application.ServiceInterfaces;
using EducationPlatform.Domain.Entities;
using EducationPlatform.Domain.Interfaces;

namespace EducationPlatform.Application.Services;

public class ParticipantService : IParticipantService
{
    private readonly IParticipantRepository _participantRepository;
    private readonly IPhonenumberRepository _phonenumberRepository;
    private readonly IEnrollmentRepository _enrollmentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ParticipantService(IParticipantRepository participantRepository, IPhonenumberRepository phonenumberRepository, IEnrollmentRepository enrollmentRepository, IUnitOfWork unitOfWork)
    {
        _participantRepository = participantRepository;
        _phonenumberRepository = phonenumberRepository;
        _enrollmentRepository = enrollmentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ParticipantResponseDTO> CreateParticipantAsync(CreateParticipantDTO participantDTO, CancellationToken cancellationToken)
    {
        if(participantDTO == null)
            throw new ArgumentNullException(nameof(participantDTO));

        var participants = await _participantRepository.GetAllAsync(cancellationToken);
        var email = participants.FirstOrDefault()?.Email;

        if (participantDTO.Email == email)
            throw new ArgumentException($"Participant with the email address - {participantDTO.Email} - already exists. Please try again.");
      

        var savedParticipant = ParticipantMapper.ToEntity(participantDTO);
        await _participantRepository.CreateAsync(savedParticipant, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return ParticipantMapper.ToDTO(savedParticipant);
    }

    public async Task<ParticipantResponseDTO> GetParticipantByEmailAsync(string email, CancellationToken cancellationToken)
    {
        if(email == null)
            throw new ArgumentNullException("Email cannot be empty");

        var participant = await _participantRepository.GetByEmailAsync(email, cancellationToken);
        if (participant == null)
            throw new KeyNotFoundException($"Could not find a participant with Email adress: {email}");

        return ParticipantMapper.ToDTO(participant);
    }

    public async Task<IReadOnlyList<ParticipantResponseDTO>> GetAllParticipantsAsync(CancellationToken cancellationToken)
    {  
        var participants = await _participantRepository.GetAllParticipantsAsync(cancellationToken);
        if (participants.Count == 0)
            return new List<ParticipantResponseDTO>();


        return participants.Select(ParticipantMapper.ToDTO).ToList();
    }

    public async Task<ParticipantResponseDTO> UpdateParticipantAsync(string email, UpdateParticipantDTO participantDTO, CancellationToken cancellationToken)
    {
        if (email == null)
            throw new ArgumentNullException("Email cannot be empty. Please try again.");

        if(participantDTO == null)  
            throw new ArgumentNullException($"Could not find a participant with Email address: {email}");

        var participantToUpdate = await _participantRepository.GetByEmailAsync(email, cancellationToken);
        if(participantToUpdate == null)
            throw new KeyNotFoundException($"Cannot find a Participant with Email address {email}.");

        ParticipantMapper.UpdateEntity(participantToUpdate, participantDTO);
        await _unitOfWork.CommitAsync(cancellationToken);

        return ParticipantMapper.ToDTO(participantToUpdate);
    }

    public async Task<bool> DeleteParticipantAsync(string email, CancellationToken cancellationToken)
    {
        if(email == null)
            throw new ArgumentNullException("Email cannot be empty. Please try again.");

        var participantToDelte = await _participantRepository.GetByEmailAsync(email, cancellationToken);
        if(participantToDelte == null)
            return false;

        var participantId = participantToDelte.Id;

        var enrollments = await _enrollmentRepository.GetAllAsync(cancellationToken);
        var enrolledParticipant = enrollments.Any(p => p.ParticipantId == participantId);

        if (enrolledParticipant)
            throw new ArgumentException("You cannot delete a enrolled participant. Please try again.");

        await _participantRepository.DeleteAsync(participantToDelte!, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return true;
    }



    // -------------------------- See notes! -----------------------------
    public async Task<PhonenumberResponseDTO> CreatePhonenumberToParticipantAsync(Guid participantId, CreatePhonenumberDTO phonenumberDTO, CancellationToken cancellationToken)
    {
        if (participantId == Guid.Empty)
            throw new ArgumentNullException(nameof(participantId));

        var participant = await _participantRepository.GetByIdAsync(participantId, cancellationToken);
        if (participant == null)
            throw new KeyNotFoundException("Could not find participant!");

        var savedPhonenumber = PhonenumberMapper.ToEntity(phonenumberDTO);
        savedPhonenumber.Participant = participant; // Explained to me by chatGPT in "InstructorService". (Sets the relation to a specific participant.)


        await _phonenumberRepository.CreateAsync(savedPhonenumber, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return PhonenumberMapper.ToDTO(savedPhonenumber);
    }

    public async Task<bool> DeletePhonenumberFromParticipantAsync(Guid participantId, string phonenumber, CancellationToken cancellationToken)
    {
        if (participantId == Guid.Empty)
            throw new ArgumentNullException(nameof(participantId));

        if(phonenumber == null)
            throw new ArgumentNullException(nameof(phonenumber));

        var phonenumbers = await _phonenumberRepository.GetByParticipantAsync(participantId, cancellationToken); // Get all phonenumber for a participant.
        var phonenumberToDelete = phonenumbers.FirstOrDefault(p => p.Phonenumber == phonenumber); // Find phonenumber to delete.
        if (phonenumberToDelete == null)
            return false;

        await _phonenumberRepository.DeleteAsync(phonenumberToDelete, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);
        return true;
    }
}
