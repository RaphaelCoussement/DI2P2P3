using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Repositories;

public class ApplicationRepository : IApplicationRepository
{
    private readonly AppDbContext _context;

    public ApplicationRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Application>> GetApplicationsAsync()
        => await _context.Applications.ToListAsync();

    public async Task<Application?> GetApplicationByIdAsync(int id)
        => await _context.Applications.FindAsync(id);

    public async Task AddApplicationAsync(Application application)
    {
        _context.Applications.Add(application);
        await _context.SaveChangesAsync();
    }
    
    public async Task DeleteApplicationAsync(int id)
    {
        var passwords = await _context.Passwords
            .Where(p => p.ApplicationId == id)
            .ToListAsync();
    
        if (passwords.Any())
        {
            _context.Passwords.RemoveRange(passwords);
        }
        
        var app = await _context.Applications.FindAsync(id);
        if (app != null)
        {
            _context.Applications.Remove(app);
            await _context.SaveChangesAsync();
        }
    }
}
