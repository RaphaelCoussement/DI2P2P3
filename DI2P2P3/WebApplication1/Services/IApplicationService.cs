using WebApplication1.Models;

namespace WebApplication1.Services;

public interface IApplicationService
{
    Task<List<Application>> GetApplicationsAsync();
    Task<Application?> GetApplicationByIdAsync(int id);
    Task AddApplicationAsync(Application application);
    Task DeleteApplicationAsync(int id);
}