# Constellation

A multi-tenant project management platform for construction companies and contractors — a Trello-style board tool purpose-built for how construction work actually gets organized, rather than a generic task tracker.

![.NET](https://img.shields.io/badge/.NET-10-512BD4?logo=dotnet&logoColor=white)
![React](https://img.shields.io/badge/React-18-61DAFB?logo=react&logoColor=black)
![TypeScript](https://img.shields.io/badge/TypeScript-5-3178C6?logo=typescript&logoColor=white)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-16-4169E1?logo=postgresql&logoColor=white)
![EF Core](https://img.shields.io/badge/EF%20Core-10-512BD4?logo=dotnet&logoColor=white)
![Docker](https://img.shields.io/badge/Docker-Compose-2496ED?logo=docker&logoColor=white)

## Overview

Construction companies manage work differently from software teams — projects are organized by phase (site prep, framing, rough-in, inspection, closeout) rather than sprints, and the tools built for generic Kanban workflows don't reflect that. Constellation applies the familiar board/column/card model construction managers already understand from tools like Trello, but with a domain shaped around how construction projects are actually structured.

Every new project automatically seeds a board with construction-relevant default columns (**Planning → In Progress → Inspection → Completed**) instead of a generic empty board — giving a manager a working starting point on day one rather than a blank canvas.

## Features

- **Multi-tenant by design** — every company operates in its own isolated data space, with the domain model built to support both shared-database (row-level tenant scoping) and dedicated-database tenancy for larger customers, without changing the application layer.
- **Company → Project → Board → Column → Task** hierarchy, fully modeled end-to-end with a REST API and a working React UI on top of it.
- **Auto-seeded project boards** — creating a project automatically provisions a default board with construction-relevant columns, via domain logic rather than a manual setup step.
- **Task assignments and comments** — assign users to tasks (with a single "lead" per task enforced as a domain invariant) and thread comments on individual tasks.
- **Live Kanban board view** — companies, projects, boards, columns, and tasks are browsable end-to-end in the UI, backed entirely by the API (no mock data).

## Architecture

The backend follows a layered/domain-driven structure:

```
Constellation.Domain          Entities, aggregates, and business rules — no framework dependencies
Constellation.Infrastructure  EF Core, PostgreSQL persistence, entity configurations
Constellation.Api             ASP.NET Core Web API, controllers, request/response DTOs
Constellation.Web             React + TypeScript frontend (Vite)
```

A few things worth highlighting for anyone reading the code:

- **Aggregates enforce their own invariants.** `Project`, `Board`, and `TaskItem` are aggregate roots that own their children (`Board`s, `BoardColumn`s, `TaskAssignment`s) through controlled methods rather than exposing mutable collections directly — e.g., a task can only ever have one assignment marked "lead," enforced in the domain method itself, not in a controller or a database constraint.
- **Tenant isolation is a first-class concern**, not bolted on. Every entity carries a `TenantId`, and the `Company` entity is deliberately designed to support routing to either a shared database (row-filtered) or a fully dedicated database per tenant, without changing how the rest of the domain is modeled.
- **The database engine was a deliberate choice, not a default.** PostgreSQL over SQL Server, decided specifically for this project's multi-tenancy and extensibility needs — cheaper per-instance footprint for provisioning isolated tenant databases, mature Row-Level Security as defense-in-depth against tenant-isolation bugs, and JSONB/PostGIS for future custom-field and geospatial features relevant to a construction domain.

## Tech Stack

**Backend:** .NET 10 · ASP.NET Core Web API · Entity Framework Core · PostgreSQL (Npgsql) · Swagger/OpenAPI

**Frontend:** React 18 · TypeScript · Vite

**Infrastructure:** Docker Compose (PostgreSQL, Redis, pgAdmin)

## Getting Started

**Prerequisites:** [.NET 10 SDK](https://dotnet.microsoft.com/download), [Node.js](https://nodejs.org/) (18+), [Docker Desktop](https://www.docker.com/products/docker-desktop/)

1. **Start the database services:**
   ```bash
   docker compose up -d
   ```

2. **Run the API** (from the repo root):
   ```bash
   dotnet run --project Constellation.Api
   ```
   Migrations apply automatically on startup in development. API runs at `http://localhost:5160`; Swagger UI is available at `/swagger`.

3. **Run the frontend** (in a separate terminal):
   ```bash
   cd Constellation.Web
   npm install
   npm run dev
   ```
   App runs at `http://localhost:5173`.

4. **Shut down** when finished:
   ```bash
   docker compose down
   ```
   (data persists in Docker volumes between sessions)

## Roadmap

Actively developed. Near-term:

- Task detail view (description, assignments, comment thread)
- Drag-and-drop for moving tasks between columns
- Authentication and per-user access control
- Construction-specific entities beyond the generic board/task model — daily logs, punch lists, RFIs, change orders

## License

Not yet licensed for reuse — portfolio/personal project.
