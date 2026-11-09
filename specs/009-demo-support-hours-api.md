# ADR-009: Demo support hours API

- Status: Proposed
- Issue: [#47](https://github.com/githubabcs-devops/parts-unlimited-web/issues/47)
- Endpoint: `GET /api/support-hours`
- Dependencies: None
- Size: Small

## Context

A small schedule endpoint demonstrates structured business information without
adding more health/version aliases or depending on the open contact-form PR.
The demo has no real staffed support service.

## Decision

Return a fixed illustrative weekly schedule as camelCase JSON:

```json
{
  "isDemo": true,
  "timeZone": "UTC",
  "days": [
    { "day": "monday", "opens": "09:00", "closes": "17:00", "isClosed": false },
    { "day": "tuesday", "opens": "09:00", "closes": "17:00", "isClosed": false },
    { "day": "wednesday", "opens": "09:00", "closes": "17:00", "isClosed": false },
    { "day": "thursday", "opens": "09:00", "closes": "17:00", "isClosed": false },
    { "day": "friday", "opens": "09:00", "closes": "17:00", "isClosed": false },
    { "day": "saturday", "opens": null, "closes": null, "isClosed": true },
    { "day": "sunday", "opens": null, "closes": null, "isClosed": true }
  ]
}
```

Use a typed immutable fixture in `src/Web` and a minimal API route in `Program.cs`.
Extend the existing JSON context if source-generated serialization has landed.
Keep explicit null fields for closed days. Do not calculate `openNow`, use the
server's local time zone, or publish contact details.

## Acceptance criteria

- [ ] The route returns HTTP 200, `application/json`, and the contract above.
- [ ] All seven days appear exactly once in Monday-to-Sunday order.
- [ ] Weekdays use 24-hour time strings; weekends have explicit null times.
- [ ] `timeZone` is `UTC` and `isDemo` is true on every request.
- [ ] README explains the endpoint and that no real support availability is promised.

## Verification

Add xUnit integration tests for the complete weekly schedule, explicit weekend
nulls, ordering, and response metadata. Run `dotnet build -c Release` and
`dotnet test src/Web.Tests/Web.Tests.csproj`.

## Alternatives and consequences

Computing current availability adds clock and time-zone edge cases for no
demo benefit. A static schedule is deterministic and independent of contact,
accounts, staffing, holidays, or external calendars.
