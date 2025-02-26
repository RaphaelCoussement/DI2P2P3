using System.Security.Cryptography;
using System.Text;

namespace WebApplication1.Services;

public class RSAEncryptionStrategy : IEncryptionStrategy
{
    private static readonly RSA rsa = RSA.Create();

    /// <summary>
    /// Chiffre le texte en clair en utilisant l'algorithme RSA.
    /// </summary>
    /// <param name="plainText">Le texte en clair à chiffrer</param>
    /// <returns>Le texte chiffré sous forme de chaîne de caractères Base64</returns>
    public string Encrypt(string plainText)
    {
        byte[] encrypted = rsa.Encrypt(Encoding.UTF8.GetBytes(plainText), RSAEncryptionPadding.Pkcs1);
        return Convert.ToBase64String(encrypted);
    }

    /// <summary>
    /// Déchiffre le texte chiffré en utilisant l'algorithme RSA.
    /// </summary>
    /// <param name="encryptedText">Le texte chiffré à déchiffrer (sous forme de chaîne Base64)</param>
    /// <returns>Le texte déchiffré en clair</returns>
    public string Decrypt(string encryptedText)
    {
        byte[] decrypted = rsa.Decrypt(Convert.FromBase64String(encryptedText), RSAEncryptionPadding.Pkcs1);
        return Encoding.UTF8.GetString(decrypted);
    }
}