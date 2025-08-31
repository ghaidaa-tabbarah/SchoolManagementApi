# SchoolManagement API

## Backend Developer Test – School Management System

This project is a **RESTful API** for a School Management System, built with **.NET 8**, **Entity Framework Core**, and **SQL Server**.

It manages users, courses, enrollments, and assignments.

---

## Project Structure

- **SchoolManagement.Presentation.API**  
  API layer: Controllers, DTOs, Middleware

- **SchoolManagement.Application**  
  Application layer: App Services and DTOs

- **SchoolManagement.Domain**  
  Domain layer: Entities, Managers, Business Rules

- **SchoolManagement.Infrastructure**  
  Infrastructure layer: EF Core DbContext, Repositories, Security

- **SchoolManagement.Migrator**  
  Responsible for:
    - Applying **database migrations**
    - Running **DataSeeder** to insert initial data (Users, Courses, Assignments, Enrollments)

---

## Users & Roles

- **Admin**
    - Manage courses
    - Enroll students in courses
- **Teacher**
    - Manage their own courses
    - Add assignments
    - Grade assignments
- **Student**
    - View their own courses
    - Submit assignments

---

## Technical Requirements

- .NET 8 Web API
- Entity Framework Core (Code-First)
- SQL Server
- JWT Authentication
- Clean Architecture (Controllers, Services, Repositories)
- Use of DTOs
- Input validation
- Logging
- Paging, Filtering, Sorting
- Async/Await with proper CancellationToken support

---

## Setup Instructions

### 1 Clone Repository
```bash
git clone https://github.com/ghaidaa-tabbarah/SchoolManagement.gi
cd SchoolManagement
```

### 2 Update Connection String

In appsettings.json (for Migrator and API):

```
"ConnectionStrings": {
  "MsSql": "Server=localhost;Database=SchoolManagement;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

### 3 Run Migrations & Seed Data
```
dotnet run --project SchoolManagement.Migrator
```

This will:

- Apply EF Core migrations
- Run DataSeeder to insert initial data into the database

### 4 Run the API
```
dotnet run --project SchoolManagement.Presentation.API
```

API will be available at:

https://localhost:5184/swagger (Swagger UI enabled)

### Seeded Users (Initial Data)

| Role    | Username       | Password       |
|---------|----------------|----------------|
| Admin   | Admin          | Admin          |
| Teacher | jane.smith     | Password123!   |
| Student | mike.johnson   | Password123!   |
| Student | sarah.wilson   | Password123!   |
