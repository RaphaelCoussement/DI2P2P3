namespace WebApplication1.Services;

public interface IEncryptionStrategy
{
    /// <summary>
    /// Chiffre un texte en clair.
    /// </summary>
    /// <param name="plainText">Le texte en clair à chiffrer</param>
    /// <returns>Le texte chiffré sous forme de chaîne de caractères (base64)</returns>
    string Encrypt(string plainText);

    /// <summary>
    /// Déchiffre un texte chiffré.
    /// </summary>
    /// <param name="encryptedText">Le texte chiffré à déchiffrer</param>
    /// <returns>Le texte en clair obtenu après déchiffrement</returns>
    string Decrypt(string encryptedText);
}