# ADR-006: Demo delivery information page

- Status: Proposed
- Issue: [#44](https://github.com/githubabcs-devops/parts-unlimited-web/issues/44)
- Route: `GET /delivery`
- Dependencies: None
- Size: Small

## Context

A storefront benefits from an easily found delivery explanation. The open
free-shipping highlight PR is promotional home-page copy, not a delivery page.
This feature must not change its offer or introduce shipping calculations.

## Decision

Add `src/Web/Pages/Delivery.cshtml` with the shared layout, page title and h1
`Delivery information`. Show a prominent notice that all delivery information
is illustrative and no real shipments are made.

Include h2 sections `Delivery estimates`, `Processing time`, and
`Tracking`. Describe standard delivery as 3-5 business days and express delivery
as 1-2 business days after a simulated dispatch. State that processing is
illustrative, weekends are excluded, and no tracking service is connected.
Do not specify prices or free-shipping thresholds.

Add a descriptive `Delivery information` footer link, preserving existing links.
Use plain semantic content with no forms, scripts, or external services.

## Acceptance criteria

- [ ] `/delivery` returns HTTP 200 with the expected title, h1, and sections.
- [ ] Both delivery estimates and the no-real-shipments notice are visible.
- [ ] The page makes no pricing, free-shipping, or real tracking promises.
- [ ] The shared footer links to `/delivery`.
- [ ] The heading hierarchy is logical and content reflows at 320 CSS pixels.

## Verification

Add xUnit rendering tests for the route, both estimates, demo notice, and
footer link. Manually check keyboard navigation and narrow-screen layout.
Run `dotnet build -c Release` and
`dotnet test src/Web.Tests/Web.Tests.csproj`.

## Alternatives and consequences

A delivery calculator needs addresses and business rules outside this scope.
Static copy is small and safe; ADR-008 uses the same illustrative estimates
without making this page depend on an API.
