# Readme - Code Samples for Chapter 20, Security

This chapter contains the following code samples:

* Authentication
    * IdentitySample (with Microsoft.Identity.Client)
    * WebAppWithADSample (ASP.NET Core with Azure AD)
* Encryption
    * X509CertificateSample (get a certificate from Azure Key Vault)
    * SigningDemo (signing using `ECDsaCng`)
    * SecureTransfer (encrypting and decrypting a message, this sample requires .NET 4.6)
* Web Security
    * ASPNETCoreMVCSecurity (encoding, injection, XSRF)

## .NET 6 Updates

Code sample X509CertificateSample:

The `Key` property (X509Certificate2.PublicKey.Key) is obsolete. The method has been changed to use the `GetRSAPublicKey` method, and accessing members of the `RSA` class instead.

## Configure Azure Key Vault

To configure the Azure Key Vault and create certificates using the Azure CLI see these bash scripts:

* [prepare key vault](preparekeyvault.sh)
* [create certificates](createcertificates.sh)

## More Information
 
For code comments and issues please check [Professional C#'s GitHub Repository](https://github.com/ProfessionalCSharp/ProfessionalCSharp2021)

Please check my blog [csharp.christiannagel.com](https://csharp.christiannagel.com "csharp.christiannagel.com") for additional information for topics covered in the book.

Thank you!