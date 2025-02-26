using WebApplication1.Dtos;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Services;

public class PasswordService : IPasswordService
{
    private readonly Dictionary<ApplicationType, IEncryptionStrategy> _strategies;
    private readonly IPasswordRepository _passwordRepository;

    /// <summary>
    /// Constructeur de la classe PasswordService.
    /// </summary>
    /// <param name="passwordRepository">Le repository pour interagir avec la base de données des mots de passe</param>
    public PasswordService(IPasswordRepository passwordRepository)
    {
        _strategies = new Dictionary<ApplicationType, IEncryptionStrategy>
        {
            { ApplicationType.GrandPublic, new AESEncryptionStrategy() },
            { ApplicationType.Professionnelle, new RSAEncryptionStrategy() }
        };
        _passwordRepository = passwordRepository;
    }

    /// <summary>
    /// Chiffre le mot de passe en fonction du type d'application.
    /// </summary>
    /// <param name="password">Le mot de passe à chiffrer</param>
    /// <param name="type">Le type d'application pour déterminer la méthode de chiffrement</param>
    /// <returns>Le mot de passe chiffré sous forme de chaîne de caractères</returns>
    public string EncryptPassword(string password, ApplicationType type)
    {
        return _strategies[type].Encrypt(password);
    }

    /// <summary>
    /// Déchiffre le mot de passe en fonction du type d'application.
    /// </summary>
    /// <param name="encryptedPassword">Le mot de passe chiffré à déchiffrer</param>
    /// <param name="type">Le type d'application pour déterminer la méthode de déchiffrement</param>
    /// <returns>Le mot de passe en clair après déchiffrement</returns>
    public string DecryptPassword(string encryptedPassword, ApplicationType type)
    {
        return _strategies[type].Decrypt(encryptedPassword);
    }
    
    /// <summary>
    /// Ajoute un mot de passe dans la base de données.
    /// </summary>
    /// <param name="password">Le mot de passe à ajouter</param>
    /// <returns>Une tâche représentant l'opération asynchrone</returns>
    public async Task AddPasswordAsync(Password password)
    {
        await _passwordRepository.AddPasswordAsync(password);
    }
    
    /// <summary>
    /// Récupère tous les mots de passe.
    /// </summary>
    /// <returns>Une liste de DTO de mots de passe avec les mots de passe déchiffrés</returns>
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
    
    /// <summary>
    /// Récupère un mot de passe par son ID.
    /// </summary>
    /// <param name="id">L'ID du mot de passe à récupérer</param>
    /// <returns>Le mot de passe correspondant à l'ID spécifié</returns>
    public async Task<Password> GetPasswordByIdAsync(int id)
    {
        return await _passwordRepository.GetPasswordByIdAsync(id);
    }

    /// <summary>
    /// Supprime un mot de passe par son ID.
    /// </summary>
    /// <param name="id">L'ID du mot de passe à supprimer</param>
    /// <returns>Une tâche représentant l'opération asynchrone</returns>
    public async Task DeletePasswordAsync(int id)
    {
        await _passwordRepository.DeletePasswordAsync(id);
    }
}
