# User Management System 🚀

A **User Management System** built with **ASP.NET Core** following **Clean Architecture** principles. The system provides authentication, role management, user profiles, and a CQRS-based structure.

## 📌 Features
✅ **Authentication & Authorization** (JWT-based)  
✅ **User Management** (CRUD operations for users)  
✅ **Role-Based Access Control (RBAC)**  
✅ **CQRS Pattern for Commands & Queries**  
✅ **Unit Testing with xUnit & Moq**  
✅ **Repository & Service Layer Separation**  
✅ **Swagger API Documentation**  

---

## 🏗️ Project Structure

UserManagementSystem/ │── UserManagementSystem.Api/ # API Layer (Controllers & Endpoints) │── UserManagementSystem.Application/ # Application Layer (Services, CQRS, DTOs) │── UserManagementSystem.Domain/ # Domain Layer (Entities & Interfaces) │── UserManagementSystem.Infrastructure/ # Infrastructure Layer (Repositories, Database) │── UserManagementSystem.Tests/ # Unit & Integration Tests


---

## 🛠️ Technologies Used
- **ASP.NET Core 8.0**
- **Entity Framework Core**
- **MediatR (CQRS)**
- **JWT Authentication**
---

📜 API Endpoints
🔑 Authentication

    POST /api/auth/login → Login & receive JWT token
    POST /api/register → Register a new user

👤 User Management

    GET /api/users → Retrieve all users
    POST /api/users → Create a user
    PUT /api/users/{id} → Update user details
    DELETE /api/users/{id} → Delete user

🎭 Role Management

    GET /api/roles → Retrieve all roles
    POST /api/roles → Create a role
    DELETE /api/roles/{id} → Delete a role

📌 Contribution Guidelines

    Fork the repository
    Create a new branch: git checkout -b feature-branch
    Commit changes: git commit -m "Added new feature"
    Push the branch: git push origin feature-branch
    Open a Pull Request

📄 License

This project is licensed under the MIT License.

📌 Developed & Maintained by Kiani66
