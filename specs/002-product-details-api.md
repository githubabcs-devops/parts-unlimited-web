# ADR-002: Product details lookup API

- Status: Proposed
- Issue: [#40](https://github.com/githubabcs-devops/parts-unlimited-web/issues/40)
- Endpoint: `GET /api/products/{id:int}`
- Dependencies: [ADR-001](001-demo-product-catalog-api.md) / [#39](https://github.com/githubabcs-devops/parts-unlimited-web/issues/39) must be implemented first
- Size: Small

## Context

Once the demo catalog exists, callers need to retrieve one product without
downloading and searching the full array. This is a focused routing and
not-found handling exercise.

## Decision

Add an integer-constrained minimal API route in `src/Web/Program.cs`. Reuse the
typed fixture introduced by ADR-001; do not copy product data into a second
collection. Return the same product shape as the list endpoint.

For example, `/api/products/1` returns:

```json
{ "id": 1, "name": "Demo Brake Pads", "category": "brakes", "price": 29.99, "currency": "USD" }
```

Unknown integer IDs, including zero and negative values, return HTTP 404 with
an `application/problem+json` body containing `status: 404` and
`title: "Product not found"`. A non-integer route segment does not match the
route and returns HTTP 404; no particular body is required in that case.

## Acceptance criteria

- [ ] IDs 1, 2, and 3 each return HTTP 200 and the corresponding catalog object.
- [ ] Missing IDs return the explicit problem response described above.
- [ ] A non-integer ID returns HTTP 404, not HTTP 500.
- [ ] The existing `GET /api/products` endpoint continues returning the full array.
- [ ] Document the route and success/not-found examples in the root README.

## Verification

Add parameterized xUnit integration cases for existing IDs, unknown IDs
`999`, `0`, and `-1`, and a non-integer segment. Compare each successful response
with its catalog entry. Run `dotnet build -c Release` and
`dotnet test src/Web.Tests/Web.Tests.csproj`.

## Alternatives and consequences

A separate repository abstraction or persistence layer is unnecessary for three
fixed products. Sharing the existing fixture prevents list/detail drift.
No product page, images, inventory checks, or write operations are included.
