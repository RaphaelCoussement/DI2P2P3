using System.Security.Cryptography;
using System.Text;

namespace WebApplication1.Services;

public class AESEncryptionStrategy : IEncryptionStrategy
{
    private readonly byte[] key = Encoding.UTF8.GetBytes("0123456789abcdef"); // 16 caractères

    /// <summary>
    /// Chiffre le texte en clair à l'aide de l'algorithme AES.
    /// </summary>
    /// <param name="plainText">Le texte en clair à chiffrer</param>
    /// <returns>Le texte chiffré sous forme de chaîne de caractères Base64, avec l'IV concaténé</returns>
    public string Encrypt(string plainText)
    {
        using Aes aes = Aes.Create();
        aes.Key = key;
        aes.GenerateIV();
        byte[] iv = aes.IV;

        using ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, iv);
        byte[] encrypted = encryptor.TransformFinalBlock(Encoding.UTF8.GetBytes(plainText), 0, plainText.Length);

        // Concaténer l'IV avec le texte chiffré et le retourner en Base64
        return Convert.ToBase64String(iv.Concat(encrypted).ToArray());
    }

    /// <summary>
    /// Déchiffre un texte chiffré à l'aide de l'algorithme AES.
    /// </summary>
    /// <param name="encryptedText">Le texte chiffré sous forme de chaîne de caractères Base64</param>
    /// <returns>Le texte en clair obtenu après déchiffrement</returns>
    public string Decrypt(string encryptedText)
    {
        byte[] fullCipher = Convert.FromBase64String(encryptedText);
        byte[] iv = fullCipher.Take(16).ToArray(); // Extraire l'IV
        byte[] cipherText = fullCipher.Skip(16).ToArray(); // Extraire le texte chiffré

        using Aes aes = Aes.Create();
        aes.Key = key;

        using ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, iv);
        byte[] decrypted = decryptor.TransformFinalBlock(cipherText, 0, cipherText.Length);

        // Convertir le tableau de bytes déchiffrés en chaîne de caractères
        return Encoding.UTF8.GetString(decrypted);
    }
}