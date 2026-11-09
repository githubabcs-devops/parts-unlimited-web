# ADR-003: Derived product categories API

- Status: Proposed
- Issue: [#41](https://github.com/githubabcs-devops/parts-unlimited-web/issues/41)
- Endpoint: `GET /api/categories`
- Dependencies: [ADR-001](001-demo-product-catalog-api.md) / [#39](https://github.com/githubabcs-devops/parts-unlimited-web/issues/39) must be implemented first
- Size: Small

## Context

Clients need a short list of available product categories. Maintaining a second
hardcoded category list would drift as the demo product fixture changes.

## Decision

Expose a read-only minimal API endpoint that derives distinct category strings
from the shared ADR-001 catalog and sorts them with ordinal ascending comparison.
For the initial fixture, return:

```json
["brakes", "filters"]
```

Use the existing JSON conventions and registration pattern. Do not introduce a
category database, configurable taxonomy, product filtering, or admin UI.

## Acceptance criteria

- [ ] `GET /api/categories` returns HTTP 200 and `application/json`.
- [ ] The initial response is exactly `["brakes", "filters"]`.
- [ ] Categories are derived from the shared catalog, without duplicated literals.
- [ ] Duplicate categories appear only once and ordering is deterministic.
- [ ] Document the endpoint and response in the root README.

## Verification

Add xUnit integration coverage for the status, media type, expected response,
and agreement with the distinct sorted categories returned by `/api/products`.
Run `dotnet build -c Release` and
`dotnet test src/Web.Tests/Web.Tests.csproj`.

## Alternatives and consequences

A fixed standalone list would be shorter but could disagree with products.
Deriving categories keeps this feature small while giving future clients a
reliable browse primitive. There are no category IDs or localization requirements.
