using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Data;

namespace WebApplication1.Repositories
{
    public class PasswordRepository : IPasswordRepository
    {
        private readonly AppDbContext _context;

        public PasswordRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Ajoute un mot de passe dans la base de données
        /// </summary>
        /// <param name="password">L'objet Password à ajouter</param>
        /// <returns>Une tâche représentant l'opération asynchrone d'ajout du mot de passe</returns>
        public async Task AddPasswordAsync(Password password)
        {
            _context.Passwords.Add(password);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Récupère tous les mots de passe de la base de données, incluant les informations sur l'application associée
        /// </summary>
        /// <returns>Une tâche représentant l'opération asynchrone qui retourne la liste des mots de passe avec leurs applications</returns>
        public async Task<IEnumerable<Password>> GetAllPasswordsAsync()
        {
            return await _context.Passwords.Include(p => p.Application).ToListAsync();
        }

        /// <summary>
        /// Récupère un mot de passe en fonction de son ID, incluant les informations sur l'application associée
        /// </summary>
        /// <param name="id">L'ID du mot de passe à récupérer</param>
        /// <returns>Une tâche représentant l'opération asynchrone qui retourne le mot de passe trouvé, ou null si non trouvé</returns>
        public async Task<Password> GetPasswordByIdAsync(int id)
        {
            return await _context.Passwords.Include(p => p.Application)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        /// <summary>
        /// Supprime un mot de passe de la base de données en fonction de son ID
        /// </summary>
        /// <param name="id">L'ID du mot de passe à supprimer</param>
        /// <returns>Une tâche représentant l'opération asynchrone de suppression du mot de passe</returns>
        public async Task DeletePasswordAsync(int id)
        {
            var password = await _context.Passwords.FindAsync(id);
            if (password != null)
            {
                _context.Passwords.Remove(password);
                await _context.SaveChangesAsync();
            }
        }
    }
}
