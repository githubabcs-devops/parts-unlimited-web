# ADR-008: Fixed demo shipping options API

- Status: Proposed
- Issue: [#46](https://github.com/githubabcs-devops/parts-unlimited-web/issues/46)
- Endpoint: `GET /api/shipping/options`
- Dependencies: None
- Size: Small

## Context

Future demo clients need a tiny shipping-method feed without address handling
or a shipping-provider integration. This is separate from the open home-page
free-shipping highlight and must not change that promotion.

## Decision

Expose a read-only minimal API endpoint returning the following fixed contract,
with numeric decimal costs and camelCase JSON:

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

Use a small typed model in `src/Web` and register the route in `Program.cs`.
If source-generated JSON support exists, extend its current context.
The values are illustrative base options, not a checkout quote or promotion.
Do not accept addresses, order totals, or payment data.

## Acceptance criteria

- [ ] The route returns HTTP 200 and `application/json`.
- [ ] The response matches the contract above, including option ordering.
- [ ] Costs are JSON numbers, estimates are integer business-day ranges,
  and `isDemo` is a boolean.
- [ ] Requests have no external service calls or state changes.
- [ ] Document the endpoint, sample response, and demo-only limitations in README.

## Verification

Add xUnit integration coverage for the complete response, numeric types,
ordering, and repeatability. Run `dotnet build -c Release` and
`dotnet test src/Web.Tests/Web.Tests.csproj`.

## Alternatives and consequences

A live rate calculator is unnecessary for this demo. Fixed data makes the API
easy to test. If ADR-006 is implemented, preserve matching delivery estimates;
neither feature requires the other to land first.
