using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

try
{
    (var aliceKey, var aliceBlob) = GetAliceKeys();
    (var bobKey, var bobBlob) = GetBobKeys();
    byte[] encrytpedData = await AliceSendsDataAsync("this is a secret message for Bob", aliceKey, bobBlob);
    await BobReceivesDataAsync(encrytpedData, bobKey, aliceBlob);
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

(CngKey aliceKey, byte[] aliceBlob) GetAliceKeys()
{
    var aliceKey = CngKey.Create(CngAlgorithm.ECDiffieHellmanP521);
    var alicePubKeyBlob = aliceKey.Export(CngKeyBlobFormat.EccPublicBlob);
    return (aliceKey, alicePubKeyBlob);
}

(CngKey bobKey, byte[] bobBlob) GetBobKeys()
{
    var bobKey = CngKey.Create(CngAlgorithm.ECDiffieHellmanP521);
    var bobPubKeyBlob = bobKey.Export(CngKeyBlobFormat.EccPublicBlob);
    return (bobKey, bobPubKeyBlob);
}

async Task<byte[]> AliceSendsDataAsync(string message, CngKey aliceKey, byte[] bobBlob)
{
    Console.WriteLine($"Alice sends message: {message}");
    byte[] rawData = Encoding.UTF8.GetBytes(message);
    byte[]? encryptedData = null;

    using var aliceAlgorithm = new ECDiffieHellmanCng(aliceKey);
    using CngKey bobPubKey = CngKey.Import(bobBlob, CngKeyBlobFormat.EccPublicBlob);
    byte[] symmKey = aliceAlgorithm.DeriveKeyMaterial(bobPubKey);
    Console.WriteLine("Alice creates this symmetric key with " +
        $"Bobs public key information: { Convert.ToBase64String(symmKey)}");
    using var aes = new AesCryptoServiceProvider();
    aes.Key = symmKey;
    aes.GenerateIV();
    using ICryptoTransform encryptor = aes.CreateEncryptor();
    using var ms = new MemoryStream();

    {
        // create CryptoStream and encrypt data to send
        using var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);

        // write initialization vector not encrypted
        await ms.WriteAsync(aes.IV, 0, aes.IV.Length);
        await cs.WriteAsync(rawData, 0, rawData.Length);
    }  // need to close the cryptostream before using the memorystream
    encryptedData = ms.ToArray();
 
    aes.Clear();
    Console.WriteLine($"Alice: message is encrypted: {Convert.ToBase64String(encryptedData)}"); ;
    Console.WriteLine();
    return encryptedData;
}

async Task BobReceivesDataAsync(byte[] encryptedData, CngKey bobKey, byte[] aliceBlob)
{
    Console.WriteLine("Bob receives encrypted data");
    byte[]? rawData = null;

    var aes = new AesCryptoServiceProvider();

    int nBytes = aes.BlockSize >> 3;
    byte[] iv = new byte[nBytes];
    for (int i = 0; i < iv.Length; i++)
    {
        iv[i] = encryptedData[i];
    }

    using var bobAlgorithm = new ECDiffieHellmanCng(bobKey);
    using CngKey alicePubKey = CngKey.Import(aliceBlob, CngKeyBlobFormat.EccPublicBlob);

    byte[] symmKey = bobAlgorithm.DeriveKeyMaterial(alicePubKey);
    Console.WriteLine($"Bob creates this symmetric key with Alices public key information: {Convert.ToBase64String(symmKey)}");
    aes.Key = symmKey;
    aes.IV = iv;

    using ICryptoTransform decryptor = aes.CreateDecryptor();
    using MemoryStream ms = new MemoryStream();
    { 
        using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Write);

        await cs.WriteAsync(encryptedData, nBytes, encryptedData.Length - nBytes);
    }  // close the cryptostream before using the memorystream
    rawData = ms.ToArray();

    Console.WriteLine($"Bob decrypts message to: {Encoding.UTF8.GetString(rawData)}");

    aes.Clear();
}
