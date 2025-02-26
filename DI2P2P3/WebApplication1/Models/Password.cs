using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models;

public class Password
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public string EncryptedPassword { get; set; } = string.Empty;

    [Required]
    public int ApplicationId { get; set; }

    [ForeignKey("ApplicationId")]
    public Application Application { get; set; } = null!;
}