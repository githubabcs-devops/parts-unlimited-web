---
applyTo: "src/**/*.cs"
---
# C# path-specific instructions — highest precedence for `src/`

These rules apply when editing C# under `src/` and **win over** `AGENTS.md` and
`.github/copilot-instructions.md` when they conflict (path-specific instructions have the highest
precedence when their `applyTo` glob matches). This file is the demo's "tie-breaker" that resolves
the intentional British-vs-American conflict in the two root instruction files.

- ✅ **User-facing strings use American English** (e.g., "color", "organize") — this is the effective rule for `src/`.
- Target **.NET 8**; enable nullable reference types and use file-scoped namespaces.
- Add **xUnit** tests in `src/Web.Tests` for new behavior; keep `dotnet build` and `dotnet test` green.
- Read configuration from environment / app settings — **never** hardcode secrets or connection strings.
- Keep changes minimal and focused; do not reformat unrelated code.
