# Book Updates

## Chapter 20, Security

Page 560, the command

> az app list --display-name ProCSharpIdentityApp --query [].appId

should be:

> az ad app list --display-name ProCSharpIdentityApp --query [].appId