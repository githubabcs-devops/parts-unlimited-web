// Provisions a Linux Azure App Service (plan + Web App for .NET) for ONE environment.
// Deployed once per environment (dev / qa / prod) by the pipeline via `az deployment group create`.
// No secrets, subscription IDs, or environment-specific names are hardcoded — everything is a parameter.

@description('Base application name (without the environment suffix).')
param appBaseName string = 'gh-ado-e2e-demo'

@description('Environment name. Drives resource names and ASPNETCORE_ENVIRONMENT.')
@allowed([
  'dev'
  'qa'
  'prod'
])
param environmentName string

@description('Azure region. Defaults to the resource group location.')
param location string = resourceGroup().location

@description('App Service plan SKU (e.g. B1 for dev/qa, P1v3 for prod).')
param sku string = 'B1'

@description('Linux .NET runtime stack.')
param linuxFxVersion string = 'DOTNETCORE|8.0'

var namePrefix = '${appBaseName}-${environmentName}'
var planName = '${namePrefix}-plan'
// Web App names must be globally unique — append a short deterministic hash from the RG id.
var webAppName = '${namePrefix}-${uniqueString(resourceGroup().id)}'

resource plan 'Microsoft.Web/serverfarms@2023-12-01' = {
  name: planName
  location: location
  sku: {
    name: sku
  }
  kind: 'linux'
  properties: {
    reserved: true
  }
}

resource webApp 'Microsoft.Web/sites@2023-12-01' = {
  name: webAppName
  location: location
  kind: 'app,linux'
  properties: {
    serverFarmId: plan.id
    httpsOnly: true
    siteConfig: {
      linuxFxVersion: linuxFxVersion
      minTlsVersion: '1.2'
      ftpsState: 'Disabled'
      appSettings: [
        {
          name: 'ASPNETCORE_ENVIRONMENT'
          value: environmentName
        }
        {
          name: 'WEBSITE_RUN_FROM_PACKAGE'
          value: '1'
        }
      ]
    }
  }
}

@description('The deployed Web App resource name (used by the pipeline to deploy the package).')
output appServiceName string = webApp.name

@description('The default hostname of the deployed Web App.')
output appServiceHostName string = webApp.properties.defaultHostName
