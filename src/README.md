# src — Sample application

A minimal but **real** ASP.NET Core (.NET 8) Razor Pages app used throughout the demo.

- **Projects:** `Web/` (the app) and `Web.Tests/` (xUnit tests).
- **Home page** (`Web/Pages/Index.cshtml`, `Web/HomeContent.cs`, `Web/Pages/Shared/_Layout.cshtml`) maps to
  the seeded user stories: hero banner, "What's New" highlights, dynamic footer year, and a `/health`
  endpoint (`Web/Program.cs`).
- **Build / test / run:**
  ```bash
  dotnet test src/Web.Tests/Web.Tests.csproj -c Release
  dotnet run --project src/Web
  ```
- The Azure Pipeline (`../pipelines/azure-pipelines.yml`) builds, tests and publishes `Web/`, then deploys
  Dev → QA → Prod. The agentic SDLC workflow ([`../agents/sdlc-team`](../agents/sdlc-team)) and the PR‑review
  app ([`../apps/pr-review-agent`](../apps/pr-review-agent)) operate on this app.

> **This is the SEED app.** `scripts/bootstrap-ado.ps1` pushes `src/` into a new **Azure DevOps** repo
> (with a sample PR + work items); GEI then migrates it to GitHub. So the same app you see here is what the
> demo migrates and deploys — see [`../docs/02-reproduce-the-demo.md`](../docs/02-reproduce-the-demo.md).
> Conventions: [`../.github/instructions/csharp.instructions.md`](../.github/instructions/csharp.instructions.md).

