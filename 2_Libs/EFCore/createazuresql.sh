#! /bin/bash

# Prepare variables
rg=rg-procsharp
loc=westeurope
servername=procsharpserver$RANDOM
databasename=procsharp$RANDOM
clientip=enter your client-ip address

# Create a resource group
az group create --location $loc --name $rg

# Create a Azure SQL Server with firewall rules
az sql server create --name $servername --resource-group $rg --location $loc --admin-user myadminuser --admin-password Pa$$w0rd
az sql server firewall-rule create -g $rg -s $servername -n azurerule --start-ip-address 0.0.0.0 --end-ip-address 0.0.0.0
az sql server firewall-rule create -g $rg -s $servername -n clientiprule --start-ip-address $clientip --end-ip-address $clientp

# Create the Azure SQL database
az sql db create -g $rg -s $servername -n $databasename --service-objective Basic
