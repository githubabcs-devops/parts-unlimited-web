---
mode: 'agent'
description: 'Implement an Azure Boards user story on the Parts Unlimited app, add a test, and open a PR.'
---
# Implement a user story

You are the coding agent for the Parts Unlimited app. Implement the referenced Azure Boards user story
on the .NET 8 app in `src/Web`.

Steps:
1. Read the user story (title, description, acceptance criteria). If given a work-item id, treat it as `AB#<id>`.
2. Create a branch named `feature/AB<id>-<slug>`.
3. Make the smallest change that satisfies the acceptance criteria, editing files under `src/Web`.
4. Add or update an xUnit test under `src/Web.Tests` that covers the change.
5. Run `dotnet test src/Web.Tests/Web.Tests.csproj` and ensure it is green.
6. Commit using Conventional Commits, referencing `AB#<id>` in the message.
7. Open a **draft** pull request; the PR-review app will review it.

Constraints:
- Follow `.github/copilot-instructions.md`, `AGENTS.md`, and `.github/instructions/csharp.instructions.md`.
- Keep changes minimal and well-scoped. Never hardcode secrets.
- User-facing strings use American English (per the path-specific instruction for `src/`).
