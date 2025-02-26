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
}
