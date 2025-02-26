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

    /// <summary>
    /// Constructeur de la classe PasswordController
    /// </summary>
    /// <param name="passwordService">Le service qui gère les mots de passe</param>
    /// <param name="applicationService">Le service qui gère les applications</param>
    public PasswordController(IPasswordService passwordService, IApplicationService applicationService)
    {
        _passwordService = passwordService;
        _applicationService = applicationService;
    }

    /// <summary>
    /// Méthode POST pour ajouter un mot de passe pour une application
    /// </summary>
    /// <param name="passwordDto">Le DTO contenant les informations du mot de passe à ajouter</param>
    /// <returns>Retourne le mot de passe ajouté avec un code HTTP 200 si réussi, ou une erreur 404 si l'application est introuvable</returns>
    [HttpPost]
    public async Task<IActionResult> AddPassword([FromBody] PasswordDTO passwordDto)
    {
        // Récupérer l'application en fonction de l'ApplicationId
        Application app = await _applicationService.GetApplicationByIdAsync(passwordDto.ApplicationId);

        // Vérifier si l'application existe
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

        // Retourne le mot de passe ajouté
        return Ok(password);
    }

    /// <summary>
    /// Méthode GET pour récupérer tous les mots de passe
    /// </summary>
    /// <returns>Retourne une liste de mots de passe avec un code HTTP 200, ou une erreur 404 si aucun mot de passe n'est trouvé</returns>
    [HttpGet]
    public async Task<IActionResult> GetAllPasswords()
    {
        var passwords = await _passwordService.GetAllPasswordsAsync();

        // Vérifier si des mots de passe existent
        if (passwords == null || !passwords.Any())
        {
            return NotFound("Aucun mot de passe trouvé.");
        }

        return Ok(passwords); // Retourne la liste des mots de passe avec un code 200
    }

    /// <summary>
    /// Méthode GET pour récupérer un mot de passe en fonction de son ID
    /// </summary>
    /// <param name="id">L'ID du mot de passe à récupérer</param>
    /// <returns>Retourne le mot de passe avec un code HTTP 200 si trouvé, ou une erreur 404 si le mot de passe est introuvable</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPasswordById(int id)
    {
        // Récupérer le mot de passe en fonction de l'ID
        Password password = await _passwordService.GetPasswordByIdAsync(id);

        // Vérifier si le mot de passe existe
        if (password == null)
        {
            return NotFound($"Mot de passe avec l'ID {id} non trouvé.");
        }

        return Ok(password); // Retourne le mot de passe avec un code 200 si trouvé
    }

    /// <summary>
    /// Méthode DELETE pour supprimer un mot de passe en fonction de son ID
    /// </summary>
    /// <param name="id">L'ID du mot de passe à supprimer</param>
    /// <returns>Retourne une réponse HTTP 204 (Pas de contenu) après suppression, ou une erreur 404 si le mot de passe est introuvable</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePassword(int id)
    {
        // Vérifier si le mot de passe existe
        Password password = await _passwordService.GetPasswordByIdAsync(id);

        // Si le mot de passe n'est pas trouvé, retourne une erreur 404
        if (password == null)
        {
            return NotFound($"Mot de passe avec l'ID {id} non trouvé.");
        }

        // Supprimer le mot de passe
        await _passwordService.DeletePasswordAsync(id);

        // Retourne une réponse HTTP 204 pour indiquer que la suppression a bien eu lieu
        return NoContent();
    }

}
