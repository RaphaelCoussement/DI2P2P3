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

    public ApplicationController(IApplicationService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetApplications()
    {
        try
        {
            var apps = await _service.GetApplicationsAsync();
            Console.WriteLine($"Nombre d'applications récupérées : {apps.Count()}"); // Debug
            return Ok(apps);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur dans GetApplications : {ex.Message}");
            return StatusCode(500, "Erreur interne du serveur");
        }
    }


    [HttpPost]
    public async Task<IActionResult> AddApplication([FromBody] ApplicationDto appDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var app = new Application
        {
            Name = appDto.Name,
            Type = appDto.Type
        };

        await _service.AddApplicationAsync(app);

        return CreatedAtAction(nameof(GetApplications), new { id = app.Id }, app);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetApplicationById(int id)
    {
        var app = await _service.GetApplicationByIdAsync(id);

        if (app == null)
        {
            return NotFound($"Application avec l'ID {id} non trouvée.");
        }

        return Ok(app);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteApplication(int id)
    {
        var app = await _service.GetApplicationByIdAsync(id);
        if (app == null)
        {
            return NotFound($"Application avec l'ID {id} non trouvée.");
        }

        await _service.DeleteApplicationAsync(id);

        return NoContent();  // Retourne un code 204 (Pas de contenu) après suppression
    }

}
