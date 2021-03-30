#!/bin/bash

# Define variables
rg=rg-procsharp
loc=westeurope
keyvaultname=procsharpvault$RANDOM

# Create the resource group
az group create --location $loc --name $rg

# Create the Key Vault
az keyvault create -n $keyvaultname -g $rg -l $loc --enable-rbac-authorization true

# After creating the key vault create a role assignment for the 'KeyVault Certificates Officer' as shown in https://docs.microsoft.com/en-us/azure/key-vault/general/rbac-guide
