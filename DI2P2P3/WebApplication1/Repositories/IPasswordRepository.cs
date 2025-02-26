using WebApplication1.Models;

namespace WebApplication1.Repositories;

public interface IPasswordRepository
{
    /// <summary>
    /// Ajoute un mot de passe à la base de données
    /// </summary>
    /// <param name="password">L'objet Password à ajouter</param>
    /// <returns>Une tâche représentant l'opération asynchrone d'ajout du mot de passe</returns>
    Task AddPasswordAsync(Password password);

    /// <summary>
    /// Récupère tous les mots de passe de la base de données
    /// </summary>
    /// <returns>Une tâche représentant l'opération asynchrone qui retourne une liste de mots de passe</returns>
    Task<IEnumerable<Password>> GetAllPasswordsAsync();

    /// <summary>
    /// Récupère un mot de passe en fonction de son ID
    /// </summary>
    /// <param name="id">L'ID du mot de passe à récupérer</param>
    /// <returns>Une tâche représentant l'opération asynchrone qui retourne le mot de passe trouvé, ou null si non trouvé</returns>
    Task<Password> GetPasswordByIdAsync(int id);

    /// <summary>
    /// Supprime un mot de passe de la base de données en fonction de son ID
    /// </summary>
    /// <param name="id">L'ID du mot de passe à supprimer</param>
    /// <returns>Une tâche représentant l'opération asynchrone de suppression du mot de passe</returns>
    Task DeletePasswordAsync(int id);
}