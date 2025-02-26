using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Services;

public class ApplicationService : IApplicationService
{
    private readonly IApplicationRepository _repository;

    /// <summary>
    /// Initialise un nouveau service pour gérer les applications.
    /// </summary>
    /// <param name="repository">Le repository utilisé pour accéder aux données des applications</param>
    public ApplicationService(IApplicationRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Récupère la liste de toutes les applications.
    /// </summary>
    /// <returns>Une liste d'applications</returns>
    public async Task<List<Application>> GetApplicationsAsync() 
        => await _repository.GetApplicationsAsync();

    /// <summary>
    /// Récupère une application en fonction de son identifiant.
    /// </summary>
    /// <param name="id">L'identifiant de l'application</param>
    /// <returns>L'application correspondante, ou null si non trouvée</returns>
    public async Task<Application?> GetApplicationByIdAsync(int id) 
        => await _repository.GetApplicationByIdAsync(id);

    /// <summary>
    /// Ajoute une nouvelle application dans la base de données.
    /// </summary>
    /// <param name="application">L'application à ajouter</param>
    public async Task AddApplicationAsync(Application application) 
        => await _repository.AddApplicationAsync(application);

    /// <summary>
    /// Supprime une application en fonction de son identifiant.
    /// </summary>
    /// <param name="id">L'identifiant de l'application à supprimer</param>
    public async Task DeleteApplicationAsync(int id)
    {
        await _repository.DeleteApplicationAsync(id);
    }
}