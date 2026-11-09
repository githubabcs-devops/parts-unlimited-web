# ADR-007: Demo returns information page

- Status: Proposed
- Issue: [#45](https://github.com/githubabcs-devops/parts-unlimited-web/issues/45)
- Route: `GET /returns`
- Dependencies: None
- Size: Small

## Context

Returns information is familiar storefront content and a useful small Razor
Page task. There is no order or payment system to integrate with.

## Decision

Add `src/Web/Pages/Returns.cshtml` with the shared layout, page title and h1
`Returns information`. Start with a visible notice that this is a fictional
policy for a demo and no actual returns or refunds are processed.

Provide h2 sections `Demo return window`, `Item condition`, and `How it would work`.
Describe an illustrative 30-day return window and unused items in original
packaging. Present an ordered list of three conceptual steps: locate the order
reference, request a return, and follow the provided instructions. Explicitly
say these steps cannot be submitted or completed in this demo.

Add a `Returns information` footer link while preserving existing links.
Do not add a form, upload, refund endpoint, contact workflow, or legal guarantee.

## Acceptance criteria

- [ ] `/returns` returns HTTP 200 with the expected title, h1, and sections.
- [ ] The 30-day window, item-condition guidance, and three steps are visible.
- [ ] Demo limitations are explicit; no actionable submission control is shown.
- [ ] The shared footer links to `/returns`.
- [ ] Headings and the ordered list are semantic and reflow at 320 CSS pixels.

## Verification

Add xUnit rendering tests for the route, demo notice, return window, ordered
steps, and footer link. Manually check keyboard access and narrow-screen layout.
Run `dotnet build -c Release` and
`dotnet test src/Web.Tests/Web.Tests.csproj`.

## Alternatives and consequences

A real returns workflow requires order identity, storage, and payment integration.
A clearly labeled informational page avoids those dependencies and collection
of personal information.
