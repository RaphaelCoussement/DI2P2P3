using WebApplication1.Dtos;
using WebApplication1.Models;

namespace WebApplication1.Services;

public interface IPasswordService
{
    /// <summary>
    /// Chiffre le mot de passe en fonction du type d'application.
    /// </summary>
    /// <param name="password">Le mot de passe à chiffrer</param>
    /// <param name="type">Le type d'application pour déterminer la méthode de chiffrement</param>
    /// <returns>Le mot de passe chiffré sous forme de chaîne de caractères</returns>
    string EncryptPassword(string password, ApplicationType type);

    /// <summary>
    /// Déchiffre le mot de passe en fonction du type d'application.
    /// </summary>
    /// <param name="encryptedPassword">Le mot de passe chiffré à déchiffrer</param>
    /// <param name="type">Le type d'application pour déterminer la méthode de déchiffrement</param>
    /// <returns>Le mot de passe en clair après déchiffrement</returns>
    string DecryptPassword(string encryptedPassword, ApplicationType type);

    /// <summary>
    /// Ajoute un mot de passe dans la base de données.
    /// </summary>
    /// <param name="password">Le mot de passe à ajouter</param>
    /// <returns>Une tâche représentant l'opération asynchrone</returns>
    Task AddPasswordAsync(Password password);

    /// <summary>
    /// Récupère tous les mots de passe.
    /// </summary>
    /// <returns>Une liste de DTO de mots de passe</returns>
    Task<IEnumerable<PasswordsDTO>> GetAllPasswordsAsync();

    /// <summary>
    /// Récupère un mot de passe par son ID.
    /// </summary>
    /// <param name="id">L'ID du mot de passe à récupérer</param>
    /// <returns>Le mot de passe correspondant à l'ID spécifié</returns>
    Task<Password> GetPasswordByIdAsync(int id);

    /// <summary>
    /// Supprime un mot de passe par son ID.
    /// </summary>
    /// <param name="id">L'ID du mot de passe à supprimer</param>
    /// <returns>Une tâche représentant l'opération asynchrone</returns>
    Task DeletePasswordAsync(int id);
}
