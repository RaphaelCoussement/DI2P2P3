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

        public async Task AddPasswordAsync(Password password)
        {
            _context.Passwords.Add(password);
            await _context.SaveChangesAsync();
        }
        
        public async Task<IEnumerable<Password>> GetAllPasswordsAsync()
        {
            return await _context.Passwords.Include(p => p.Application).ToListAsync();  
        }
        
        public async Task<Password> GetPasswordByIdAsync(int id)
        {
            return await _context.Passwords.Include(p => p.Application)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

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