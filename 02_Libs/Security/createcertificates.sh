#!/bin/bash

# Define variables
rg=rg-procsharp
loc=westeurope
keyvaultname={enter your key vault name}
alicecert="AliceCert"
bobcert="BobCert"

# Create a certificate for Alice
az keyvault certificate create -n $alicecert --vault-name $keyvaultname --policy "$(az keyvault certificate get-default-policy)"

# Create a certificate for Bob
az keyvault certificate create -n $bobcert --vault-name $keyvaultname --policy "$(az keyvault certificate get-default-policy)"
