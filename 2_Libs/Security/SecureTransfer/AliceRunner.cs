sealed class AliceRunner : IDisposable
{
    private readonly ILogger _logger;
    private ECDiffieHellman _algorithm;
    public AliceRunner(ILogger<AliceRunner> logger)
    {
        _logger = logger;
        _algorithm = ECDiffieHellman.Create();
        _logger.LogInformation($"Using this ECDiffieHellman class: {_algorithm.GetType().Name}");
    }

    public void Dispose() => _algorithm.Dispose();

    public ECDiffieHellmanPublicKey GetPublicKey() => _algorithm.PublicKey;

    public async Task<(byte[] Iv, byte[] EncryptedData)> GetSecretMessageAsync(ECDiffieHellmanPublicKey otherPublicKey)
    {
        string message = "secret message from Alice";
        _logger.LogInformation($"Alice sends message {message}");

        byte[] plainData = Encoding.UTF8.GetBytes(message);

        byte[] symmKey = _algorithm.DeriveKeyMaterial(otherPublicKey);
        _logger.LogInformation($"Alice creates this symmetric key with " +
            $"Bobs public key information: {Convert.ToBase64String(symmKey)}");

        using Aes aes = Aes.Create();
        _logger.LogInformation($"Using this Aes class: {aes.GetType().Name}");
        aes.Key = symmKey;
        aes.GenerateIV();
        using ICryptoTransform encryptor = aes.CreateEncryptor();
        using MemoryStream ms = new();
        using (CryptoStream cs = new(ms, encryptor, CryptoStreamMode.Write))
        {
            await cs.WriteAsync(plainData.AsMemory());
        } // need to close the CryptoStream before using the MemoryStream
        byte[] encryptedData = ms.ToArray();
        _logger.LogInformation($"Alice: message is encrypted: {Convert.ToBase64String(encryptedData)}");
        var returnData = (aes.IV, encryptedData);
        aes.Clear();
        return returnData;
    }
}

