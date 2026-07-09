# Parts Unlimited Web (sample app)

Sample .NET 8 web app for the ADO -> GitHub demo. Migrated to GitHub with GEI and deployed to Azure App Service (Dev/QA/Prod).

## Endpoints

- `GET /health` — health check used by monitoring and the pipeline.
- `GET /version2` — returns the running build version as JSON, e.g. `{ "service": "parts-unlimited-web", "version": "1.0.0.0" }`, so operators can confirm what is deployed.
