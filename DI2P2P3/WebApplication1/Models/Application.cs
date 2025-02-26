using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models;

public class Application
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public ApplicationType Type { get; set; }
}

public enum ApplicationType
{
    GrandPublic, // Chiffrement AES
    Professionnelle // Chiffrement RSA
}