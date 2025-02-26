using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Repositories;

public class ApplicationRepository : IApplicationRepository
{
    private readonly AppDbContext _context;

    /// <summary>
    /// Constructeur du repository pour l'application
    /// </summary>
    /// <param name="context">Le contexte de la base de données</param>
    public ApplicationRepository(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Récupère la liste de toutes les applications de la base de données
    /// </summary>
    /// <returns>Retourne une liste d'applications</returns>
    public async Task<List<Application>> GetApplicationsAsync()
        => await _context.Applications.ToListAsync();

    /// <summary>
    /// Récupère une application en fonction de son ID
    /// </summary>
    /// <param name="id">L'ID de l'application à récupérer</param>
    /// <returns>Retourne l'application correspondante si elle est trouvée, sinon null</returns>
    public async Task<Application?> GetApplicationByIdAsync(int id)
        => await _context.Applications.FindAsync(id);

    /// <summary>
    /// Ajoute une nouvelle application à la base de données
    /// </summary>
    /// <param name="application">L'objet Application à ajouter</param>
    /// <returns>Une tâche représentant l'opération asynchrone d'ajout de l'application</returns>
    public async Task AddApplicationAsync(Application application)
    {
        _context.Applications.Add(application);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Supprime une application et ses mots de passe associés de la base de données
    /// </summary>
    /// <param name="id">L'ID de l'application à supprimer</param>
    /// <returns>Une tâche représentant l'opération asynchrone de suppression</returns>
    public async Task DeleteApplicationAsync(int id)
    {
        // Récupérer tous les mots de passe associés à l'application
        var passwords = await _context.Passwords
            .Where(p => p.ApplicationId == id)
            .ToListAsync();
    
        // Supprimer tous les mots de passe associés à l'application
        if (passwords.Any())
        {
            _context.Passwords.RemoveRange(passwords);
        }
        
        // Récupérer l'application à supprimer
        var app = await _context.Applications.FindAsync(id);
        if (app != null)
        {
            // Supprimer l'application
            _context.Applications.Remove(app);
            await _context.SaveChangesAsync();
        }
    }
}
