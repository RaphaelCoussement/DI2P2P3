using System.Security.Cryptography;
using System.Text;

namespace WebApplication1.Services;

public class AESEncryptionStrategy : IEncryptionStrategy
{
    private readonly byte[] key = Encoding.UTF8.GetBytes("0123456789abcdef"); // 16 caractères

    public string Encrypt(string plainText)
    {
        using Aes aes = Aes.Create();
        aes.Key = key;
        aes.GenerateIV();
        byte[] iv = aes.IV;

        using ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, iv);
        byte[] encrypted = encryptor.TransformFinalBlock(Encoding.UTF8.GetBytes(plainText), 0, plainText.Length);

        return Convert.ToBase64String(iv.Concat(encrypted).ToArray());
    }

    public string Decrypt(string encryptedText)
    {
        byte[] fullCipher = Convert.FromBase64String(encryptedText);
        byte[] iv = fullCipher.Take(16).ToArray();
        byte[] cipherText = fullCipher.Skip(16).ToArray();

        using Aes aes = Aes.Create();
        aes.Key = key;

        using ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, iv);
        byte[] decrypted = decryptor.TransformFinalBlock(cipherText, 0, cipherText.Length);

        return Encoding.UTF8.GetString(decrypted);
    }
}