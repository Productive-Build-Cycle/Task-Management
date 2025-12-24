# Project Decisions – Task & Work Item Service

This document captures domain and technical decisions made by the team for the MVP.
The goal is clarity: reduce ambiguity, keep the scope focused, and make future changes easier.

---

## 1) Domain Decisions

### 1.1 Task Fields (MVP)
| Field | Required | Notes |
|------|----------|------|
| Id | ✅ | GUID |
| Title | ✅ | max 200 chars |
| Description | ❌ | optional |
| Status | ✅ | default: `Todo` |
| Priority | ✅ | default: `Medium` |
| Assignee | ❌ | simple identifier (string or guid) |
| DueDate | ❌ | UTC |
| CreatedAt | ✅ | UTC |
| UpdatedAt | ✅ | UTC |

---

## 2) Status Flow (State Machine)

### Allowed statuses
- `Todo`
- `Doing`
- `Done`

### Allowed transitions
| From | To | Allowed |
|------|----|---------|
| Todo | Doing | ✅ |
| Doing | Done | ✅ |
| Todo | Done | ❌ |
| Done | * | ❌ |
| Doing | Todo | ❌ |

**Decision:** Backward transitions are NOT allowed in MVP.

**After Done:**
- Status cannot be changed
- Assignee cannot be changed
- DueDate cannot be changed

Reason: keep MVP simple and rules strict for clear domain enforcement.

---

## 3) Due Date Rules
- DueDate is optional
- DueDate in the past is **NOT allowed**
- All dates stored in **UTC**
- No automatic status changes based on DueDate (no background jobs in MVP)

---

## 4) Priority Rules
Supported priorities:
- `Low`
- `Medium`
- `High`

Defaults:
- new tasks default to `Medium`

List default sorting:
- Priority (High → Low), then DueDate (earlier first)

---

## 5) API Decisions (MVP)
HTTP codes:
- `400` Validation errors
- `404` Not found
- `409` Conflict (invalid status transition / invalid state change)
- `500` Unexpected errors

---

## 6) Data Decisions
- SQL Server + EF Core
- Store `Status` and `Priority` as **int enums**
- Indexes:
  - Status
  - Priority
  - DueDate
  - Assignee (if frequently filtered)

**Concurrency (MVP):** not implemented (can be added later via RowVersion).

---

## 7) Explicitly Out of Scope
- AuthN/AuthZ
- Roles
- Notifications
- Audit/history
- Background jobs
