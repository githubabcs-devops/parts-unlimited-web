# ADR-010: Plain-language parts glossary page

- Status: Proposed
- Issue: [#48](https://github.com/githubabcs-devops/parts-unlimited-web/issues/48)
- Route: `GET /glossary`
- Dependencies: None
- Size: Small

## Context

A small educational page makes the storefront more recognizable without
requiring checkout, catalog integration, or external product content.
It also demonstrates meaningful native HTML semantics.

## Decision

Add `src/Web/Pages/Glossary.cshtml` using the shared layout, page title and
single h1 `Parts glossary`. Render a native `dl` with five `dt`/`dd` pairs
in this order:

1. `Air filter`: Helps keep dust and debris out of the engine's air intake.
2. `Brake pads`: Friction components that press against brake rotors to slow a vehicle.
3. `Oil filter`: Helps remove contaminants from circulating engine oil.
4. `Spark plug`: Creates the spark used to ignite fuel in many gasoline engines.
5. `Wiper blade`: Clears rain and debris from the windshield.

Use original plain-language wording based on these meanings. Include a short
notice that the page is general demo information, not installation instructions
or vehicle-specific compatibility advice.
Add a `Parts glossary` footer link, preserving all existing links.

## Acceptance criteria

- [ ] `/glossary` returns HTTP 200 with the expected title and h1.
- [ ] All five terms have nonempty definitions in native description-list markup.
- [ ] The definitions appear in the order specified above.
- [ ] The general-information notice is visible, with no installation procedure.
- [ ] The footer links to `/glossary`; content reflows at 320 CSS pixels.

## Verification

Add xUnit rendering tests for the route, terms and definitions, description-list
markup, disclaimer, and footer link. Manually check keyboard navigation and
narrow-screen layout. Run `dotnet build -c Release` and
`dotnet test src/Web.Tests/Web.Tests.csproj`.

## Alternatives and consequences

A searchable knowledge base or CMS would be excessive for five definitions.
A static semantic page is independent of the catalog API and adds no packages,
JavaScript, third-party text, images, or maintenance service.
