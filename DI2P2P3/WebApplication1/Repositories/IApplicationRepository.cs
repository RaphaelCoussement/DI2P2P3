using WebApplication1.Models;

namespace WebApplication1.Repositories;

public interface IApplicationRepository
{
    /// <summary>
    /// Récupère la liste de toutes les applications
    /// </summary>
    /// <returns>Une tâche représentant l'opération asynchrone qui retourne une liste d'applications</returns>
    Task<List<Application>> GetApplicationsAsync();

    /// <summary>
    /// Récupère une application en fonction de son ID
    /// </summary>
    /// <param name="id">L'ID de l'application à récupérer</param>
    /// <returns>Une tâche représentant l'opération asynchrone qui retourne l'application trouvée, ou null si non trouvée</returns>
    Task<Application?> GetApplicationByIdAsync(int id);

    /// <summary>
    /// Ajoute une nouvelle application à la base de données
    /// </summary>
    /// <param name="application">L'objet Application à ajouter</param>
    /// <returns>Une tâche représentant l'opération asynchrone d'ajout de l'application</returns>
    Task AddApplicationAsync(Application application);

    /// <summary>
    /// Supprime une application de la base de données en fonction de son ID
    /// </summary>
    /// <param name="id">L'ID de l'application à supprimer</param>
    /// <returns>Une tâche représentant l'opération asynchrone de suppression</returns>
    Task DeleteApplicationAsync(int id);
}