sealed class AliceRunner(ILogger<AliceRunner> logger) : IDisposable
{
    private readonly ILogger _logger = logger;
    private readonly ECDsa _signAlgorithm = ECDsa.Create();

    public void Dispose() => _signAlgorithm.Dispose();

    public byte[] GetPublicKey() => _signAlgorithm.ExportSubjectPublicKeyInfo();

    public (byte[] Data, byte[] Sign) GetDocumentAndSignature()
    {
        _logger.LogInformation($"Using this ECDsa class: {_signAlgorithm.GetType().Name}");

        byte[] aliceData = Encoding.UTF8.GetBytes("I'm Alice");
        byte[] aliceDataSignature = _signAlgorithm.SignData(aliceData, HashAlgorithmName.SHA512);
        return (aliceData, aliceDataSignature);
    }
}
  