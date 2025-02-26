using WebApplication1.Models;

namespace WebApplication1.Repositories;

public interface IPasswordRepository
{

    Task AddPasswordAsync(Password password);
    Task<IEnumerable<Password>> GetAllPasswordsAsync();
    Task<Password> GetPasswordByIdAsync(int id); 
    Task DeletePasswordAsync(int id);
}