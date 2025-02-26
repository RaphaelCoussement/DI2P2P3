using System.Security.Cryptography;
using System.Text;

namespace WebApplication1.Services;

public class RSAEncryptionStrategy : IEncryptionStrategy
{
    private static readonly RSA rsa = RSA.Create();

    public string Encrypt(string plainText)
    {
        byte[] encrypted = rsa.Encrypt(Encoding.UTF8.GetBytes(plainText), RSAEncryptionPadding.Pkcs1);
        return Convert.ToBase64String(encrypted);
    }

    public string Decrypt(string encryptedText)
    {
        byte[] decrypted = rsa.Decrypt(Convert.FromBase64String(encryptedText), RSAEncryptionPadding.Pkcs1);
        return Encoding.UTF8.GetString(decrypted);
    }
}