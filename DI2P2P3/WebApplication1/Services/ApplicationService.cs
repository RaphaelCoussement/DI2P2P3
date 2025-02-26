using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Services;

public class ApplicationService : IApplicationService
{
    private readonly IApplicationRepository _repository;

    public ApplicationService(IApplicationRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Application>> GetApplicationsAsync() => await _repository.GetApplicationsAsync();
    public async Task<Application?> GetApplicationByIdAsync(int id) => await _repository.GetApplicationByIdAsync(id);
    public async Task AddApplicationAsync(Application application) => await _repository.AddApplicationAsync(application);
    public async Task DeleteApplicationAsync(int id)
    {
        await _repository.DeleteApplicationAsync(id);
    }
}