#!/bin/bash

# Define variables
rg=rg-procsharp-24
loc=westeurope
appservice=procsharpapp$RANDOM
webapp=procsharpwebapp$RANDOM
acrname=procsharpregistry$RANDOM
installpath=".bin/Release/net5.0/publish"
webappname=websampleapp/v1.0

# Create the resource group
az group create --location $loc --name $rg

# Create the App Service Plan
az appservice plan create -g $rg -n $appservice --is-linux --number-of-workers 1 --sku S1

# Create the Web App
# az webapp create -g $rg -p $appservice -n $webapp -r "DOTNET|5.0"

# Publish the Web App (az webapp deploy is in preview and currently only supports war and static files)
# az webapp deploy -g $rg -n $webapp --src-path $installpath --type TBD --async true

# Create an Azure Container Registry
az acr create -g $rg -n $acrname --sku Standard -l $loc 

# Get the Link to the ACR
acrurl=$(az acr show -g $rg -n $acrname --query "loginServer")

targetname="${acrurl}/${webappname}"

# build the docker image
docker build . -t $webappname

# tag the docker image to push it to the ACR
docker tag $webappname $targetname

# login with the ACR
az acr login -n $acrurl

# push the docker image to the ACR
docker push $targetname

# You might need to give the webapp permissions to access the registry! You can use managed identities!

# Create the Web App with Docker
az webapp create -g $rg -p $appservice -n $webapp -i $targetname
