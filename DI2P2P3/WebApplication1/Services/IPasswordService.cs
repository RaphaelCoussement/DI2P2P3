using WebApplication1.Dtos;
using WebApplication1.Models;

namespace WebApplication1.Services;

public interface IPasswordService
{
    // Méthode pour chiffrer le mot de passe en fonction du type d'application
    string EncryptPassword(string password, ApplicationType type);

    // Méthode pour déchiffrer le mot de passe en fonction du type d'application
    string DecryptPassword(string encryptedPassword, ApplicationType type);
    Task AddPasswordAsync(Password password);
    Task<IEnumerable<PasswordsDTO>> GetAllPasswordsAsync();
    Task<Password> GetPasswordByIdAsync(int id);  // Nouvelle méthode pour obtenir un mot de passe par ID
    Task DeletePasswordAsync(int id);
}