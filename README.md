# Gym Management System

A RESTful API built with ASP.NET Core for managing end-to-end gym operations — members, subscriptions, attendance, employees, scheduling, leave management, and automated SMS notifications.

## Features

- **Member Management** — register and manage gym members
- **Subscriptions** — multiple subscription types with expiry tracking and renewal handling
- **Attendance** — track attendance for both members and employees
- **Employee Management** — role-based employees (Trainers & Receptionists)
- **Scheduling** — unified scheduling service with role-filtered logic; Trainers are assigned to classes, Receptionists follow regular shifts
- **Leave Management** — request and track employee leave with annual leave cap validation
- **Profit Tracking** — track gym revenue based on membership activity
- **SMS Notifications** — automated subscription-expiry alerts via Twilio, built with the Observer Pattern and a Background Worker (Event-Driven Architecture)
- **Authentication & Authorization** — JWT-based authentication with role-based access control

## Tech Stack

| Category | Technology |
|---|---|
| Framework | ASP.NET Core |
| Architecture | Clean Architecture, Repository Pattern |
| Data Access | Entity Framework Core |
| Database | SQL Server |
| Authentication | JWT |
| Mapping | AutoMapper |
| Validation | FluentValidation |
| Notifications | Twilio (SMS) |
| Testing | xUnit |
| Patterns | Observer Pattern, Background Service |

## Architecture

The project follows **Clean Architecture**, separating concerns into distinct layers:

- **Domain** — core entities and business rules
- **Application** — services, DTOs, interfaces
- **Infrastructure** — EF Core, repositories, external integrations (Twilio)
- **API** — controllers and presentation logic

### Key Design Decisions

- **Unified Scheduling Service** — rather than duplicating logic for Trainers and Receptionists, a single scheduling service handles both with role-filtered logic, keeping the codebase DRY.
- **Event-Driven SMS Notifications** — combines the Observer Pattern with a `BackgroundService` (`SubscriptionExpiryWorker`). When a member's subscription nears expiry, an event is published and handled asynchronously, triggering an SMS via Twilio — decoupling notification logic from the core business flow.

## Testing

Unit tests cover the **Service Layer** using **xUnit**, validating core business logic and edge cases.

## Authentication

All endpoints are secured with **JWT Authentication** and **Role-Based Authorization** (Admin, Trainer, Receptionist roles).

## Getting Started

### Prerequisites
- .NET SDK
- SQL Server

### Setup
```bash
git clone <repo-url>
cd GymManagementSystem
dotnet restore
dotnet ef database update
dotnet run
```

Update the connection string and Twilio credentials in `appsettings.json` before running.

## License

This project is for educational and portfolio purposes.
