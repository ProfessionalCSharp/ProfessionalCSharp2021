using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

class BobRunner
{
    private readonly ILogger _logger;
    private ECDiffieHellman _algorithm;
    public BobRunner(ILogger<BobRunner> logger)
    {
        _logger = logger;
        _algorithm = ECDiffieHellman.Create();
    }

    public void Dispose() => _algorithm.Dispose();

    public ECDiffieHellmanPublicKey GetPublicKey() => _algorithm.PublicKey;

    public async Task ReadMessageAsync(byte[] iv, byte[] encryptedData, ECDiffieHellmanPublicKey otherPublicKey)
    {
        _logger.LogInformation("Bob receives encrypted data");
        byte[] symmKey = _algorithm.DeriveKeyMaterial(otherPublicKey);
        _logger.LogInformation($"Bob creates this symmetric key with " +
            $"Alice public key information: {Convert.ToBase64String(symmKey)}");

        Aes aes = Aes.Create();
        aes.Key = symmKey;
        aes.IV = iv;
        using ICryptoTransform decryptor = aes.CreateDecryptor();
        using MemoryStream ms = new();
        using (CryptoStream cs = new(ms, decryptor, CryptoStreamMode.Write))
        {
            await cs.WriteAsync(encryptedData.AsMemory());
        } // close the cryptostream before using the memorystream 
        byte[] rawData = ms.ToArray();
        _logger.LogInformation($"Bob decrypts message to: {Encoding.UTF8.GetString(rawData)}");
        aes.Clear();
    }
}
