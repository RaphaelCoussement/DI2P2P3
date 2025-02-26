using WebApplication1.Models;

namespace WebApplication1.Repositories;

public interface IPasswordRepository
{

    Task AddPasswordAsync(Password password);
    Task<IEnumerable<Password>> GetAllPasswordsAsync();
}