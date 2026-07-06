# infra — Azure App Service (Bicep), 3 environments

`main.bicep` provisions a Linux **App Service plan + Web App for .NET** for **one** environment. The
pipeline deploys it three times — **dev → qa → prod** — so each environment gets its own plan and app.

## What it creates (per environment)
- `Microsoft.Web/serverfarms` — Linux App Service plan (`<appBaseName>-<env>-plan`)
- `Microsoft.Web/sites` — Web App (`<appBaseName>-<env>-<hash>`), `httpsOnly`, TLS 1.2, run-from-package
- App setting `ASPNETCORE_ENVIRONMENT=<env>`
- Outputs: `appServiceName`, `appServiceHostName`

## Parameters
| Param | Default | Notes |
|---|---|---|
| `appBaseName` | `gh-ado-e2e-demo` | base name (no env suffix) |
| `environmentName` | — | `dev` \| `qa` \| `prod` |
| `location` | resource group location | Azure region |
| `sku` | `B1` | e.g. `P1v3` for prod |
| `linuxFxVersion` | `DOTNETCORE|8.0` | runtime stack |

## Manual deploy (optional — the pipeline does this per stage)
```bash
az group create -n rg-gh-ado-e2e-demo-dev -l eastus
az deployment group create -g rg-gh-ado-e2e-demo-dev -f infra/main.bicep \
  -p appBaseName=gh-ado-e2e-demo environmentName=dev sku=B1
```

Resource group and subscription are supplied at deploy time (never committed). See
[`../pipelines/README.md`](../pipelines/README.md) for the 3-environment pipeline.
