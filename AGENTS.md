# AGENTS.md — agent onboarding for parts-unlimited-web

This repository is the **Parts Unlimited** sample web app, migrated from Azure DevOps to GitHub.
Coding agents — the **GitHub Copilot coding agent**, **Copilot CLI**, and the custom agents in
[`.github/agents/`](.github/agents/) — read this file to understand the project. Human contributors should read it too.

## What this repo is

- A small **.NET 8** web app (Razor Pages + minimal API) for the Parts Unlimited storefront.
- **Planning stays in Azure Boards.** Work items are referenced from commits and PRs as `AB#<id>`.
- **Deployment** runs through **Azure Pipelines** to Azure App Service across **Dev → QA → Prod**.

## Layout

| Path | What lives here |
| --- | --- |
| `src/Web/` | The app: `Program.cs` (endpoints), `HomeContent.cs` (home-page copy), `Pages/` (Razor). |
| `src/Web.Tests/` | xUnit tests. Every behavior change ships with a test here. |
| `pipelines/` | `azure-pipelines.yml` — Build → Dev → QA → Prod. |
| `infra/` | Bicep for the App Service environments. |
| `.github/agents/` | **Custom agents** (feature-implementer, test-author, pr-reviewer, security-auditor). |
| `.github/instructions/` | Path-specific rules (e.g. `csharp.instructions.md` for `src/**/*.cs`). |
| `.github/prompts/` | Reusable `/`-prompts (e.g. `/implement-user-story`). |

## Build, test, run

```bash
dotnet restore
dotnet build -c Release
dotnet test src/Web.Tests/Web.Tests.csproj
dotnet run --project src/Web
```

## Conventions

- Keep changes **surgical** — minimal diffs, no reformatting of unrelated code.
- **Every behavior change ships with a test.** Keep `dotnet build` and `dotnet test` green.
- **Never commit secrets.** Configuration comes from environment variables / App settings only.
- Use **Conventional Commits**, and reference the work item as `AB#<id>` in the message and PR body.

## For autonomous agents (coding agent + custom agents)

- Branch naming: `feature/AB<work-item-id>-<slug>` — this preserves the Azure Boards link via `AB#<id>`.
- Open PRs as **draft** first; the PR-review app reviews automatically. Address **CRITICAL** findings before merge.
- Pick the right specialist in [`.github/agents/`](.github/agents/): **feature-implementer** to build a story,
  **test-author** to expand coverage, **pr-reviewer** to review, **security-auditor** for a security pass.

## Precedence demo (intentional)

> ⚠️ The line below **intentionally conflicts** with `.github/copilot-instructions.md` to demonstrate
> instruction precedence live. Both files are merged as *primary*, so a direct conflict is resolved
> non-deterministically — which is exactly why path-specific files in `.github/instructions/`
> (highest precedence when their `applyTo` glob matches) are used to make intent unambiguous.

- **User-facing strings use British English** (e.g., "colour", "organise", "behaviour").
