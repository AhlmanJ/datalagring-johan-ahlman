
/* 
 * Tests-code made by chatGPT where i have asked the AI ​​to explain each row and i have also run all the tests.
 * During the tests I discovered that my ArgumentExpressions did not always return the expected Exception-messages,
 * so i made some changes to the ArgumentExpressions in each service.
 */

using EducationPlatform.Application.Abstractions.Persistence;
using EducationPlatform.Application.DTOs.Instructors;
using EducationPlatform.Application.Services;
using EducationPlatform.Domain.Entities;
using EducationPlatform.Domain.Interfaces;
using EducationPlatform.Domain.Repositories;

using NSubstitute;
using System.Linq.Expressions;

namespace EducationPlatform.Tests.Unit.Application.Instructor;

public class Instructor_Tests
{
    // NOTE! This test is created by ChatGPT but i have asked AI to break-down the code and explain it to me. See notes in the code:
    [Fact]
    public async Task CreateInstructor_Should_Return_InstructorResponseDTO_If_Instructor_Was_Created()
    {
        // Arrange

        var instructorRepository = Substitute.For<IInstructorRepository>(); // "Mock" for the Instructor-repository.
        var lessonRepository = Substitute.For<ILessonRepository>(); // "Mock" for the Lesson-repository.
        var unitOfWork = Substitute.For<IUnitOfWork>(); // "Mock" for Unit Of Work.

        var instructorService = new InstructorService(instructorRepository, lessonRepository, unitOfWork); // Instantiate the Instructor-service.

        var createDTO = new CreateInstructorDTO // Creates a simulated "CreateDto" to use in the service.
           (
                FirstName: "testFirstname",
                LastName: "testLastname",
                Email: "testEmail@domain.com",
                Expertise: "testExpert"
            );

        instructorRepository
            .ExistsAsync(Arg.Any<Expression<Func<InstructorsEntity, bool>>>(),
            Arg.Any<CancellationToken>())
            .Returns(false); // Simulates that no instructor exists yet for the given email

        // Act

        var result = await instructorService.CreateInstructorAsync(createDTO, CancellationToken.None); // Runs the actual service-method for test.

        // Assert
        Assert.NotNull(result); // Assert that the result from service is not null.
        Assert.Equal(createDTO.FirstName.Trim().ToLower(), result.FirstName); // Assert that the result contains the transformed values from input (Trim + ToLower)
        Assert.Equal(createDTO.LastName.Trim().ToLower(), result.LastName); // Assert that the result contains the transformed values from input (Trim + ToLower)
        Assert.Equal(createDTO.Email.Trim().ToLower(), result.Email); // Assert that the result contains the transformed values from input (Trim + ToLower)
        Assert.Equal(createDTO.Expertise.Trim().ToLower(), result.Expertise); // Assert that the result are the same as the input.

        await instructorRepository.Received(1).ExistsAsync(Arg.Any<Expression<Func<InstructorsEntity, bool>>>(), Arg.Any<CancellationToken>()); // Verify that ExistsAsync was called exactly once
        await instructorRepository.Received(1).CreateAsync(Arg.Any<InstructorsEntity>(), Arg.Any<CancellationToken>()); // Verify that CreateAsync was called exactly once.
        await unitOfWork.Received(1).CommitAsync(Arg.Any<CancellationToken>()); // Verify that CommitAsync was called exactly once
    }

    [Fact]
    public async Task Create_Instructor_Should_ThrowException_If_InstructorEmail_Already_Exists() 
    {
        // Arrange

        var instructorRepository = Substitute.For<IInstructorRepository>(); // "Mock" for the Instructor-repository.
        var lessonRepository = Substitute.For<ILessonRepository>(); // "Mock" for the Lesson-repository.
        var unitOfWork = Substitute.For<IUnitOfWork>(); // "Mock" for Unit Of Work.

        var instructorService = new InstructorService(instructorRepository, lessonRepository, unitOfWork); // Instantiate the Instructor-service.

        var createDTO = new CreateInstructorDTO // Creates a simulated "CreateDto" to use in the service.
           (
                FirstName: "testFirstname",
                LastName: "testLastname",
                Email: "testEmail@domain.com",
                Expertise: "testExpert"
            );

        instructorRepository
            .ExistsAsync(Arg.Any<Expression<Func<InstructorsEntity, bool>>>(),
            Arg.Any<CancellationToken>())
            .Returns(true); // Simulates that instructor email already exists in the database.

        // Act & Assert
        var ex = await Assert.ThrowsAsync<ArgumentException>(async () =>
            await instructorService.CreateInstructorAsync(createDTO, CancellationToken.None)
        ); // Runs the method in service and awaits the lambda-expression being executed to throw exception. 

        Assert.Equal("Instructor allready exist. Please try again.", ex.Message); // Verifying the same ex.message from service.

        // Verify that CreateAsync and CommitAsync is never called.
        await instructorRepository.DidNotReceive().CreateAsync(Arg.Any<InstructorsEntity>(), Arg.Any<CancellationToken>());
        await unitOfWork.DidNotReceive().CommitAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Create_Instructor_Should_ThrowException_If_CreateInstructorDTO_Is_Null()
    {
        // Arrange

        var instructorRepository = Substitute.For<IInstructorRepository>(); // "Mock" for the Instructor-repository.
        var lessonRepository = Substitute.For<ILessonRepository>(); // "Mock" for the Lesson-repository.
        var unitOfWork = Substitute.For<IUnitOfWork>(); // "Mock" for Unit Of Work.

        var instructorService = new InstructorService(instructorRepository, lessonRepository, unitOfWork); // Instantiate the Instructor-service.

        CreateInstructorDTO? createDTO = null; // Creates a simulated null "CreateDto" to use in the service.
      

        // Act & Assert
        var ex = await Assert.ThrowsAsync<ArgumentException>(async () =>
            await instructorService.CreateInstructorAsync(createDTO!, CancellationToken.None)
        ); // Runs the method in service and awaits the lambda-expression being executed to throw exception. 

        Assert.Equal("Instructor cannot be empty. Please try again.", ex.Message); // Verifying the same ex.message from service.

        // Verify that ExistsAsync, CreateAsync and CommitAsync is never called.
        await instructorRepository.DidNotReceive().ExistsAsync(Arg.Any<Expression<Func<InstructorsEntity, bool>>>(), Arg.Any<CancellationToken>());
        await instructorRepository.DidNotReceive().CreateAsync(Arg.Any<InstructorsEntity>(), Arg.Any<CancellationToken>());
        await unitOfWork.DidNotReceive().CommitAsync(Arg.Any<CancellationToken>());
    }
}
