# pipelines — Azure Pipelines (phase 1), 3 environments

`azure-pipelines.yml` builds the .NET web app **from the GitHub repo** and deploys it progressively to
**Dev → QA → Prod** Azure App Services. It is modeled on the existing `CICD-webapp-dotnet` pipeline
(definition 72) and reuses `templates/deploy-env.yml` for each environment.

```
Build ──► Deploy_Dev ──► Deploy_QA ──► Deploy_Prod
(restore/build/test/     (Bicep provision + AzureWebApp deploy, per environment)
 publish/zip artifact)
```

## One-time setup
1. **Create the pipeline** in Azure DevOps from `pipelines/azure-pipelines.yml` (select the GitHub repo).
2. **GitHub service connection** — so Azure Pipelines can build from the GitHub repo (phase 1).
3. **ARM service connection** to your Azure subscription (Workload Identity Federation preferred); set the
   `azureServiceConnection` variable to its name.
4. **ADO Environments** `Dev`, `QA`, `Prod` (Pipelines → Environments) — add approvals/checks
   (e.g. a manual approval before `Prod`). If you already have `Dev-Yaml / QA-Yaml / Prod-Yaml`, set the
   `adoEnvironment` parameter of each stage to those names instead.

## Variables
| Variable | Example | Notes |
|---|---|---|
| `azureServiceConnection` | `<arm-service-connection>` | ARM service connection name |
| `appBaseName` | `gh-ado-e2e-demo` | base name for resources |
| `location` | `eastus` | Azure region |

Per-environment resource groups default to `rg-<appBaseName>-<env>` and are created if missing.
SKUs default to `B1` for dev/qa and `P1v3` for prod (adjust in the stage parameters).

## What each deploy stage does
1. `checkout: self` (for `infra/main.bicep`) and downloads the `app` artifact.
2. `az deployment group create` → provisions the App Service for that environment (idempotent).
3. `AzureWebApp@1` → deploys the published package to the provisioned Web App.

Infrastructure details: [`../infra/README.md`](../infra/README.md).
