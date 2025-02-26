using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Dtos;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PasswordController : ControllerBase
{
    private readonly IPasswordService _passwordService;
    private readonly IApplicationService _applicationService;

    public PasswordController(IPasswordService passwordService, IApplicationService applicationService)
    {
        _passwordService = passwordService;
        _applicationService = applicationService;
    }

    [HttpPost]
    public async Task<IActionResult> AddPassword([FromBody] PasswordDTO passwordDto)
    {
        // Récupérer l'application en fonction de l'ApplicationId
        Application app = await _applicationService.GetApplicationByIdAsync(passwordDto.ApplicationId);

        if (app == null)
        {
            return NotFound("Application non trouvée");
        }

        // Créer l'objet Password en utilisant le DTO
        Password password = new Password
        {
            ApplicationId = passwordDto.ApplicationId,
            EncryptedPassword = _passwordService.EncryptPassword(passwordDto.EncryptedPassword, app.Type)
        };

        // Ajouter le mot de passe dans la base de données
        await _passwordService.AddPasswordAsync(password);

        return Ok(password);
    }
    [HttpGet]
    public async Task<IActionResult> GetAllPasswords()
    {
        var passwords = await _passwordService.GetAllPasswordsAsync();

        if (passwords == null || !passwords.Any())
        {
            return NotFound("Aucun mot de passe trouvé.");
        }

        return Ok(passwords);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPasswordById(int id)
    {
        // Récupérer le mot de passe en fonction de l'ID
        Password password = await _passwordService.GetPasswordByIdAsync(id);

        if (password == null)
        {
            return NotFound($"Mot de passe avec l'ID {id} non trouvé.");
        }

        return Ok(password);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePassword(int id)
    {
        // Vérifier si le mot de passe existe
        Password password = await _passwordService.GetPasswordByIdAsync(id);

        if (password == null)
        {
            return NotFound($"Mot de passe avec l'ID {id} non trouvé.");
        }

        // Supprimer le mot de passe
        await _passwordService.DeletePasswordAsync(id);

        return NoContent();  // Retourne un code 204 (Pas de contenu) après suppression
    }

}
