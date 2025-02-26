namespace WebApplication1.Dtos;

public class PasswordsDTO
{
    public int Id { get; set; }
    public int ApplicationId { get; set; }
    public string EncryptedPassword { get; set; } = string.Empty;
}