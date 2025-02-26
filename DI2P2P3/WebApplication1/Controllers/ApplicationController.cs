using Microsoft.AspNetCore.Mvc;
using WebApplication1.Dtos;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ApplicationController : ControllerBase
{
    private readonly IApplicationService _service;

    /// <summary>
    /// Constructeur de la classe ApplicationController
    /// Le service est injecté via la dépendance IApplicationService
    /// </summary>
    /// <param name="service">Le service qui gère la logique des applications</param>
    public ApplicationController(IApplicationService service)
    {
        _service = service;
    }

    /// <summary>
    /// Méthode GET pour récupérer la liste des applications
    /// </summary>
    /// <returns>Retourne une liste d'applications avec un code HTTP 200 si réussi, ou une erreur 500 en cas d'échec</returns>
    [HttpGet]
    public async Task<IActionResult> GetApplications()
    {
        try
        {
            var apps = await _service.GetApplicationsAsync();
            Console.WriteLine($"Nombre d'applications récupérées : {apps.Count()}"); // Debug
            return Ok(apps); // Retourne une réponse HTTP 200 avec les applications
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur dans GetApplications : {ex.Message}");
            return StatusCode(500, "Erreur interne du serveur"); // Retourne une erreur HTTP 500 en cas de problème
        }
    }

    /// <summary>
    /// Méthode POST pour ajouter une nouvelle application
    /// </summary>
    /// <param name="appDto">Les informations de l'application à ajouter</param>
    /// <returns>Retourne une réponse HTTP 201 avec l'URL de la nouvelle ressource</returns>
    [HttpPost]
    public async Task<IActionResult> AddApplication([FromBody] ApplicationDto appDto)
    {
        // Vérification de la validité du modèle
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState); // Retourne une erreur HTTP 400 si le modèle est invalide
        }

        var app = new Application
        {
            Name = appDto.Name,
            Type = appDto.Type
        };

        // Appel au service pour ajouter l'application
        await _service.AddApplicationAsync(app);

        // Retourne une réponse HTTP 201 avec l'URL de la nouvelle ressource
        return CreatedAtAction(nameof(GetApplications), new { id = app.Id }, app);
    }

    /// <summary>
    /// Méthode GET pour récupérer une application par son ID
    /// </summary>
    /// <param name="id">L'ID de l'application à récupérer</param>
    /// <returns>Retourne l'application si elle est trouvée, sinon une erreur 404</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetApplicationById(int id)
    {
        var app = await _service.GetApplicationByIdAsync(id);

        // Si l'application n'est pas trouvée, retourne une erreur 404
        if (app == null)
        {
            return NotFound($"Application avec l'ID {id} non trouvée.");
        }

        // Retourne l'application avec un code 200 si trouvée
        return Ok(app);
    }

    /// <summary>
    /// Méthode DELETE pour supprimer une application par son ID
    /// </summary>
    /// <param name="id">L'ID de l'application à supprimer</param>
    /// <returns>Retourne une réponse HTTP 204 (Pas de contenu) après suppression ou une erreur 404 si l'application n'est pas trouvée</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteApplication(int id)
    {
        var app = await _service.GetApplicationByIdAsync(id);
        
        // Si l'application n'est pas trouvée, retourne une erreur 404
        if (app == null)
        {
            return NotFound($"Application avec l'ID {id} non trouvée.");
        }

        // Appel au service pour supprimer l'application
        await _service.DeleteApplicationAsync(id);

        // Retourne une réponse HTTP 204 pour indiquer que la suppression a bien eu lieu
        return NoContent();
    }
}
