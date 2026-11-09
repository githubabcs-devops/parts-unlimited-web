# ADR-001: Read-only demo product catalog API

- Status: Proposed
- Issue: [#39](https://github.com/githubabcs-devops/parts-unlimited-web/issues/39)
- Endpoint: `GET /api/products`
- Dependencies: None
- Size: Small

## Context

The app has a home page and operational endpoints, but no product data.
A tiny deterministic catalog gives the storefront a useful API without a database.
The existing open PRs focus on presentation, contact/privacy, and operations.

## Decision

Add a read-only, in-memory catalog with three fictional products. Keep its typed
model and fixture in `src/Web`, separate from route registration in `Program.cs`.
Use decimal prices, integer IDs, camelCase JSON, and a stable ascending ID order.
The public response is a JSON array with exactly these initial entries:

```json
[
  { "id": 1, "name": "Demo Brake Pads", "category": "brakes", "price": 29.99, "currency": "USD" },
  { "id": 2, "name": "Demo Oil Filter", "category": "filters", "price": 9.99, "currency": "USD" },
  { "id": 3, "name": "Demo Air Filter", "category": "filters", "price": 14.99, "currency": "USD" }
]
```

Keep the backing collection immutable to callers. No new package, database,
authentication, external service, query filters, or write endpoint is needed.
If source-generated JSON support has landed, register the new response types
with its existing context rather than replacing that infrastructure.

## Acceptance criteria

- [ ] `GET /api/products` returns HTTP 200 and `application/json`.
- [ ] The response matches the array above, including numeric prices and ordering.
- [ ] Repeated requests return the same catalog without exposing mutable state.
- [ ] Existing health, version, and home routes retain their current behavior.
- [ ] Document the endpoint and a sample response in the root README.

## Verification

Add xUnit integration coverage in `src/Web.Tests` using the existing
`WebApplicationFactory<Program>` pattern. Assert the status, media type, complete
fixture values, and ordering. Run `dotnet build -c Release` and
`dotnet test src/Web.Tests/Web.Tests.csproj`.

## Alternatives and consequences

A database-backed catalog would be more realistic but adds setup and persistence
outside this demo's scope. An in-memory fixture is predictable and can be reused
by ADR-002 and ADR-003; it is not production inventory or pricing.
