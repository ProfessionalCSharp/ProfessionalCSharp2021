#!/bin/bash

# Prepare variables
rg=rg-procsharp
loc=westeurope
conf=ProCSharpConfig$Random
key1=AppConfigurationSample:Settings:Config1
val1="configuration value for key 1"
devval1="development value for key 1"
stagingval1="staging value for key 1"
prodval1="production value for key 1"
sentinelKey=AppConfigurationSample:Settings:Sentinel
sentinelValue=1

# Create a resource group
az group create --location $loc --name $rg

# Create an Azure App Configuration resource
az appconfig create --location $loc --name $conf --resource-group $rg 

# Create configuration values
az appconfig kv set -n $conf --key $key1 --value "$val1" --yes
az appconfig kv set -n $conf --key $key1 --label Development --value "$devval1" --yes
az appconfig kv set -n $conf --key $key1 --label Staging --value "$stagingval1" --yes
az appconfig kv set -n $conf --key $key1 --label Production --value "$prodval1" --yes
az appconfig kv set -n $conf --key $sentinelKey --value $sentinelValue --yes
