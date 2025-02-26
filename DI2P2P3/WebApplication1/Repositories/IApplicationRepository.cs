using WebApplication1.Models;

namespace WebApplication1.Repositories;

public interface IApplicationRepository
{
    Task<List<Application>> GetApplicationsAsync();
    Task<Application?> GetApplicationByIdAsync(int id);
    Task AddApplicationAsync(Application application);
}