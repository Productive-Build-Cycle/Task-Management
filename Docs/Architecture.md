\# Architecture – Lightweight Clean Architecture (DDD-lite)



This project uses a simplified Clean Architecture to keep development fast while keeping domain rules centralized and testable.



---



\## 1) Layers Overview



\### API

\- Controllers (HTTP)

\- Request/Response models (DTOs)

\- No business rules here



\### Application

\- Use-cases orchestration (Create/Assign/ChangeStatus/etc.)

\- Transactions boundary

\- Calls Domain methods and persists via repositories



\### Domain

\- Entities + business rules

\- State machine logic (Status Flow)

\- Domain exceptions / invariants

\- NO EF Core, NO DbContext references



\### Infrastructure

\- EF Core DbContext + mapping

\- Migrations

\- Repository implementations

\- SQL Server specifics



---



\## 2) Dependencies Rule

Allowed direction:

\- API → Application → Domain

\- Infrastructure → Application \& Domain (implements interfaces)



Domain has \*\*zero dependencies\*\* on other layers.



---



\## 3) Suggested Folder Structure





