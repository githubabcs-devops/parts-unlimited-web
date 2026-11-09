# ADR-005: Accessible frequently asked questions page

- Status: Proposed
- Issue: [#43](https://github.com/githubabcs-devops/parts-unlimited-web/issues/43)
- Route: `GET /faq`
- Dependencies: None
- Size: Small

## Context

Visitors need quick answers about the sample storefront. A native HTML
disclosure pattern demonstrates accessible interaction without a UI library.

## Decision

Add `src/Web/Pages/Faq.cshtml` using the shared layout, with page title and
one h1 of `Frequently asked questions`. Include four native
`details`/`summary` pairs, initially closed, with these questions:

1. `Can I place a real order?` Explain that orders and payments are not supported.
2. `Are the products real?` Explain that any displayed catalog is fictional demo data.
3. `Do I need an account?` Explain that the demo does not require an account.
4. `Can I enter personal information?` Tell visitors not to enter real personal
   or sensitive information into demo forms.

Do not link to unimplemented pages or promise a contact service. Add a
`Frequently asked questions` footer link, preserving existing links.
Keep native disclosure keyboard behavior and visible browser focus indicators.

## Acceptance criteria

- [ ] `/faq` returns HTTP 200 with the expected title and a single h1.
- [ ] All four questions and useful answers are rendered on the server.
- [ ] Each question uses `summary` within `details` without redundant ARIA.
- [ ] All disclosures work with keyboard alone and without JavaScript.
- [ ] The shared footer links to `/faq`, and content reflows at 320 CSS pixels.

## Verification

Add xUnit rendering tests for the route, four question/answer pairs, native
disclosure markup, and footer link. Manually test Tab, Enter, and Space.
Run `dotnet build -c Release` and
`dotnet test src/Web.Tests/Web.Tests.csproj`.

## Alternatives and consequences

A custom JavaScript accordion introduces focus and state handling for no benefit
here. Native disclosures keep the change small and progressively enhanced.
No search, editable content, analytics, or account functionality is included.
