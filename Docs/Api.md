\# API Reference (MVP)



> Swagger is the primary reference. This doc summarizes the endpoints and core behaviors.



---



\## Base URL

`/api`



---



\## Endpoints



\### 1) Create Task

\- `POST /tasks`

\- Creates a new task with default status `Todo`



Request (example):

```json

{

&nbsp; "title": "Implement status flow",

&nbsp; "description": "Add domain rules for Todo->Doing->Done",

&nbsp; "priority": "Medium",

&nbsp; "dueDateUtc": "2026-01-10T12:00:00Z",

&nbsp; "assignee": "user-123"

}



