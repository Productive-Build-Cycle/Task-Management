# 🗂️ Task & Work Item Service

A standalone **RESTful API** for managing **Tasks (Work Items)** built with **.NET 10** and **SQL Server**.  
The main focus is enforcing **business rules** around the task lifecycle (a simple **State Machine**) while keeping the codebase clean, maintainable, and team-friendly.

> ⚠️ This project is for **practice & learning** only and has **no personal or commercial benefit** for anyone.

---

## 🎯 Goal
Manage the full lifecycle of tasks:
- Create tasks
- Assign tasks
- Enforce status transitions
- Support due date and priority for sorting/filtering

---

## ✅ Scope (MVP)
### Core Features
- **Create Task** with input validation
- **Assign / Reassign Task**
- **Status Flow**: `Todo`, `Doing`, `Done`
- **Due Date** validation (based on team decisions)
- **Priority**: `Low`, `Medium`, `High`

### Status Flow (State Machine)
