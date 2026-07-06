using './main.bicep'

// Example parameters for the DEV environment. The pipeline passes these per stage
// (dev / qa / prod); this file is handy for a manual `az deployment group create`.
param appBaseName = 'gh-ado-e2e-demo'
param environmentName = 'dev'
param sku = 'B1'
// location defaults to the resource group location
// linuxFxVersion defaults to 'DOTNETCORE|8.0'
