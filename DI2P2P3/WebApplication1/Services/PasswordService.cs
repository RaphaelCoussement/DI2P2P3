using WebApplication1.Dtos;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Services;

public class PasswordService : IPasswordService
{
    private readonly Dictionary<ApplicationType, IEncryptionStrategy> _strategies;
    private readonly IPasswordRepository _passwordRepository;

    public PasswordService(IPasswordRepository passwordRepository)
    {
        _strategies = new Dictionary<ApplicationType, IEncryptionStrategy>
        {
            { ApplicationType.GrandPublic, new AESEncryptionStrategy() },
            { ApplicationType.Professionnelle, new RSAEncryptionStrategy() }
        };
        _passwordRepository = passwordRepository;
    }

    public string EncryptPassword(string password, ApplicationType type)
    {
        return _strategies[type].Encrypt(password);
    }

    public string DecryptPassword(string encryptedPassword, ApplicationType type)
    {
        return _strategies[type].Decrypt(encryptedPassword);
    }
    
    public async Task AddPasswordAsync(Password password)
    {
        await _passwordRepository.AddPasswordAsync(password);
    }
    
    public async Task<IEnumerable<PasswordsDTO>> GetAllPasswordsAsync()
    {
        var passwords = await _passwordRepository.GetAllPasswordsAsync();

        var passwordDtos = passwords.Select(password => new PasswordsDTO
        {
            Id = password.Id,
            ApplicationId = password.ApplicationId,
            EncryptedPassword = DecryptPassword(password.EncryptedPassword, password.Application.Type)
        }).ToList();

        return passwordDtos;
    }
    
    public async Task<Password> GetPasswordByIdAsync(int id)
    {
        return await _passwordRepository.GetPasswordByIdAsync(id);
    }

    public async Task DeletePasswordAsync(int id)
    {
        await _passwordRepository.DeletePasswordAsync(id);
    }

}
