# Parts Unlimited Web (sample app)

Sample .NET 8 web app for the ADO -> GitHub demo. Migrated to GitHub with GEI and deployed to Azure App Service (Dev/QA/Prod).

## Endpoints

- `GET /health` — health check used by monitoring and the pipeline.
- `GET /version2` — returns the running build version as JSON, e.g. `{ "service": "parts-unlimited-web", "version": "1.0.0.0" }`, so operators can confirm what is deployed.
- `GET /api/shipping/options` — returns HTTP 200 with `application/json` and fixed demo shipping options:

  ```json
  {
    "isDemo": true,
    "currency": "USD",
    "options": [
      { "id": "standard", "name": "Standard", "cost": 4.99, "minBusinessDays": 3, "maxBusinessDays": 5 },
      { "id": "express", "name": "Express", "cost": 9.99, "minBusinessDays": 1, "maxBusinessDays": 2 }
    ]
  }
  ```

  Costs and business-day estimates are illustrative base options, not a checkout
  quote or promotion. This read-only endpoint takes no inputs, accepts no addresses,
  order totals, or payment data, and makes no external service calls or state changes.
  No real shipping provider is connected or shipments made. The home-page
  free-shipping promotion is unchanged.
