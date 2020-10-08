using System;
using System.Security.Cryptography;
using System.Text;

(var aliceKeys, var alicePublicKey) = GetAliceKeys();

byte[] aliceData = Encoding.UTF8.GetBytes("Alice");
byte[] aliceSignature = CreateSignature(aliceData, aliceKeys);
Console.WriteLine($"Alice created signature: {Convert.ToBase64String(aliceSignature)}");

if (VerifySignature(aliceData, aliceSignature, alicePublicKey))
{
    Console.WriteLine("Alice signature verified successfully");
}

(CngKey KeyPair, byte[] PublicKey) GetAliceKeys()
{
    var aliceKeyPair = CngKey.Create(CngAlgorithm.ECDsaP521);
    var alicePublicKey = aliceKeyPair.Export(CngKeyBlobFormat.GenericPublicBlob);
    return (aliceKeyPair, alicePublicKey); 
}

byte[] CreateSignature(byte[] data, CngKey key)
{
    byte[] signature;
    using (var signingAlg = new ECDsaCng(key))
    {
        signature = signingAlg.SignData(data, HashAlgorithmName.SHA512);
        signingAlg.Clear();
    }
    return signature;
}

bool VerifySignature(byte[] data, byte[] signature, byte[] pubKey)
{
    bool retValue = false;
    using CngKey key = CngKey.Import(pubKey, CngKeyBlobFormat.GenericPublicBlob);
    using var signingAlg = new ECDsaCng(key);
    retValue = signingAlg.VerifyData(data, signature, HashAlgorithmName.SHA512);
    signingAlg.Clear();
    return retValue;
}
