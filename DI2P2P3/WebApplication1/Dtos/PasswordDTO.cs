using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Dtos;

public class PasswordDTO
{
    public int ApplicationId { get; set; }
    public string EncryptedPassword { get; set; } = string.Empty;
}
