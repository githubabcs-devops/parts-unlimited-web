# Copilot instructions — parts-unlimited-web

Repository-wide custom instructions for GitHub Copilot (Chat, coding agent, and code review) on the
**Parts Unlimited** sample app.

## Purpose

This is a **.NET 8** storefront web app migrated from Azure DevOps to GitHub. Optimise for clarity,
safety, and small reviewable changes. When in doubt, prefer the smallest change that satisfies the request.

## Coding standards

- **C# (.NET 8)** for the app in `src/Web`; **xUnit** for tests in `src/Web.Tests`.
- Keep changes minimal and well-scoped. Do not add dependencies without a clear need.
- Every behavior change ships with a test. Keep `dotnet build` and `dotnet test` green.
- Prefer readable, self-documenting code; comment only where intent is non-obvious.
- Enable nullable reference types; use file-scoped namespaces.

## Pull requests

- Small, focused PRs with a clear title and description.
- Link the Azure Boards work item with `AB#<id>` in the PR body.
- The PR-review app reviews every PR; resolve **CRITICAL** findings before merge.

## Security

- **Never** hardcode secrets, tokens, subscription IDs, tenant IDs, or personal data.
- Configuration is provided via environment variables / App settings only.

## Custom agents

Specialist agents live in `.github/agents/`. Use **feature-implementer** to turn a work item into a
tested change, **test-author** to raise coverage, **pr-reviewer** to review a PR, and
**security-auditor** for a focused security pass.

## Precedence demo (intentional)

> ⚠️ The line below **intentionally conflicts** with `AGENTS.md`. Both are merged as *primary*; when
> they disagree the outcome is non-deterministic. Path-specific `.github/instructions/*.instructions.md`
> files win when their `applyTo` glob matches, so we use those to make the real rule unambiguous.

- **User-facing strings use American English** (e.g., "color", "organize", "behavior").
