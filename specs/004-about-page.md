# ADR-004: About this demo page

- Status: Proposed
- Issue: [#42](https://github.com/githubabcs-devops/parts-unlimited-web/issues/42)
- Route: `GET /about`
- Dependencies: None
- Size: Small

## Context

The home page advertises the demo, but visitors have no dedicated explanation
of its purpose. Open PRs already cover home styling and navigation, so this
feature should add content without redesigning either.

## Decision

Add a Razor Page at `src/Web/Pages/About.cshtml`, with a minimal PageModel only
if needed by the existing page pattern. Reuse the shared layout and set
`ViewData["Title"]` to `About this demo`.

Use one h1, `About this demo`, and h2 sections `What this app demonstrates`
and `Demo limitations`. Explain that Parts Unlimited is a sample .NET 8
storefront used to demonstrate AI-assisted development and delivery. State
clearly that it does not accept real orders or payments.

Add a descriptive `About this demo` footer link while preserving existing links.
Use semantic HTML and existing styling; add no JavaScript, packages, personal
information, infrastructure identifiers, or claims about production capabilities.

## Acceptance criteria

- [ ] `/about` returns HTTP 200 with the expected title, h1, and both sections.
- [ ] The page explicitly says real orders and payments are not supported.
- [ ] The shared footer provides a descriptive link to `/about`.
- [ ] Content has logical headings and reflows at 320 CSS pixels.
- [ ] Existing footer links and the home page are preserved.

## Verification

Add xUnit rendering tests for the route, page title, headings, limitation copy,
and footer link. Manually check keyboard access and narrow-screen layout.
Run `dotnet build -c Release` and
`dotnet test src/Web.Tests/Web.Tests.csproj`.

## Alternatives and consequences

Adding more home-page copy would conflict with ongoing presentation work.
A separate static page is independently reviewable and has no runtime services
or configuration requirements.
