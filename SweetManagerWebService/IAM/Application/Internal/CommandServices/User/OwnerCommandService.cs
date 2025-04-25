using sweetmanager.API.Shared.Domain.Repositories;
using SweetManagerWebService.IAM.Application.Internal.OutboundServices;
using SweetManagerWebService.IAM.Domain.Model.Aggregates;
using SweetManagerWebService.IAM.Domain.Model.Commands;
using SweetManagerWebService.IAM.Domain.Model.Commands.Authentication.User;
using SweetManagerWebService.IAM.Domain.Model.Exceptions;
using SweetManagerWebService.IAM.Domain.Repositories.Credential;
using SweetManagerWebService.IAM.Domain.Repositories.Users;
using SweetManagerWebService.IAM.Domain.Services.Users.Owner;

namespace SweetManagerWebService.IAM.Application.Internal.CommandServices.User;

public class OwnerCommandService(IUnitOfWork unitOfWork, 
    IOwnerRepository ownerRepository, 
    IHashingService hashingService,
    IOwnerCredentialRepository OwnerCredentialRepository,
    ITokenService tokenService) : IOwnerCommandService
{
    public async Task<bool> Handle(SignUpUserCommand command)
    {
        try
        {
            if (await ownerRepository.FindByEmail(command.Email) is not null)
                throw new EmailAlreadyExistException();
            
            // Add Owner 
            await ownerRepository.AddAsync(new Owner(command.Id, command.Username, command.Name, command.Surname, 1
                ,command.Phone,
                command.Email, command.State));

            await unitOfWork.CompleteAsync();

            return true;
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred while creating the user: {ex.Message}");
        }
    }

    public async Task<bool> Handle(UpdateUserCommand command)
    {
        try
        {
            var result = false;

            if (await ownerRepository.FindById(command.Id) is null)
                throw new Exception($"There's no owner with the given id: {command.Id}");
            
            if (command.Change.ToUpper() == "PHONE")
            {
                result = await ownerRepository.ExecuteUpdateOwnerPhoneAsync(Convert.ToInt32(command.Value), command.Id);

                await unitOfWork.CompleteAsync();
            }
            else
            {
                result = await ownerRepository.ExecuteUpdateOwnerEmailAsync(command.Value, command.Id);

                await unitOfWork.CompleteAsync();
            }

            return result;
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred while updating the user: {ex.Message}");
        }
    }

    public async Task<dynamic?> Handle(SignInCommand command)
    {
        try
        {
            var user = await ownerRepository.FindByEmail(command.Email);

            if (user is null)
                throw new EmailDoesntExistException();

            var userCredential = await OwnerCredentialRepository.FindByOwnersIdAsync(user.Id);

            if (!hashingService.VerifyHash(command.Password, userCredential!.Code[..24], userCredential!.Code[24..]))
                throw new InvalidPasswordException();

            var hotel = await ownerRepository.FindHotelIdByOwnerId(user.Id);

            var token = tokenService.GenerateToken(new
            {
                Id = user.Id,
                PasswordHash = userCredential.Code,
                Role = "ROLE_OWNER",
                Hotel = hotel
            });

            return new
            {
                User = user,
                Token = token
            };
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}