using WebApplication1.Models;

namespace WebApplication1.Services;

public interface IApplicationService
{
    /// <summary>
    /// Récupère la liste de toutes les applications.
    /// </summary>
    /// <returns>Une liste d'applications</returns>
    Task<List<Application>> GetApplicationsAsync();

    /// <summary>
    /// Récupère une application en fonction de son identifiant.
    /// </summary>
    /// <param name="id">L'identifiant de l'application</param>
    /// <returns>L'application correspondante, ou null si non trouvée</returns>
    Task<Application?> GetApplicationByIdAsync(int id);

    /// <summary>
    /// Ajoute une nouvelle application.
    /// </summary>
    /// <param name="application">L'application à ajouter</param>
    Task AddApplicationAsync(Application application);

    /// <summary>
    /// Supprime une application en fonction de son identifiant.
    /// </summary>
    /// <param name="id">L'identifiant de l'application à supprimer</param>
    Task DeleteApplicationAsync(int id);
}