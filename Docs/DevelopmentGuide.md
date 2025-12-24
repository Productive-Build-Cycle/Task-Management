\# Development Guide (Team Workflow)



This guide defines how the 3-person team collaborates: branching, PR rules, code review, and merge strategy.



---



\## 1) Branching Model (Simple)

\- `main`: stable branch, only updated via PR

\- `develop`: integration branch for the team

\- `feature/\*`: each task/feature is implemented on a feature branch



Flow:

1\. Create `feature/<name>` from `develop`

2\. Open PR into `develop`

3\. After review + checks, merge into `develop`

4\. Final release: PR `develop` → `main`



---



\## 2) Pull Request Rules

Every PR must:

\- Be small and focused (prefer < 300–500 lines changed)

\- Have a clear title and description

\- Include screenshots/logs if relevant (e.g., Swagger changes)

\- Pass build and tests



Review:

\- At least \*\*1 reviewer\*\* must approve before merge



---



\## 3) Conflict Handling

If a PR has conflicts:

\- The PR author should merge `develop` into their feature branch

\- Resolve conflicts locally

\- Push updated branch

\- Re-request review



---



\## 4) Definition of Done (per work item)

A work item is done when:

\- Code is implemented

\- Domain rules enforced (if applicable)

\- Unit tests added/updated (for domain rules)

\- Build passes

\- PR approved and merged



---



\## 5) Coding Conventions

\- Keep controllers thin

\- Domain rules in Domain layer only

\- Prefer explicit naming over clever abstractions

\- Log errors with trace id context (if available)



