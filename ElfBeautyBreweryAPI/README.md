# 🍺 elfBeautyBrewery API – Project Structure & Design Decisions

## 📌 Project Introduction

In this project, we have built the **elfBeautyBrewery API** using .NET 9 and C# 13.0. The goal was to create a secure, maintainable, and scalable web API for brewery data, following modern software architecture principles.

---

## 🏗️ Project Structure

This project is organized into several logical layers and folders to promote separation of concerns and ease of maintenance:

- **ElfBeautyBreweryAPI**  
  This is the main ASP.NET Core Web API project. Here, we have placed the entry point (`Program.cs`), middleware (such as error handling), controllers for authentication and brewery operations, configuration files, and logging setup.

- **elfBeautyBrewery.Api.Application**  
  In this layer, we have implemented the business logic. It contains services like `BreweryService` and their interfaces, as well as domain models representing brewery entities.

- **elfBeautyBrewery.Api.Application.DataAccess**  
  This layer is responsible for data access. We have applied the repository pattern here, with interfaces and implementations such as `IBreweryRepository` and `BreweryRepository`, allowing for flexible data retrieval and easier unit testing.

- **elfBeautyBrewery.Application.Contracts**  
  Here, we have defined Data Transfer Objects (DTOs) and request/response models, such as `BreweriesSearchRequest` and `LoginRequestModel`, which help decouple API contracts from domain models.

- **elfBeautyBrewery.Application.Api.Domain**  
  This project contains the core domain entities, such as `Brewery`, representing the business data.

- **Common & Utilities**  
  Shared components like AutoMapper profiles and middleware are placed in dedicated folders for reusability.

---

## 🧩 Design Decisions

In this project, we have made several key design decisions to ensure quality and extensibility:

- **Layered Architecture**  
  We have separated the API, business logic, data access, and contracts into distinct projects and folders. This improves maintainability, scalability, and testability.

- **Dependency Injection**  
  All services and repositories are injected via constructors, supporting loose coupling and making unit testing straightforward.

- **Repository Pattern**  
  Data access is abstracted through interfaces, making it easy to swap implementations or mock for testing.

- **DTOs & AutoMapper**  
  We use DTOs and AutoMapper to map between domain models and API contracts, ensuring a clean separation and reducing manual mapping code.

- **Middleware**  
  Error handling is centralized using custom middleware, providing consistent error responses and simplifying exception management.

- **Caching**  
  In-memory caching is implemented in the business layer to optimize performance for frequently accessed data.

- **JWT Authentication**  
  The API is secured using JWT Bearer tokens, with authentication and authorization configured in the startup.

- **API Versioning & Swagger**  
  All endpoints are versioned, and Swagger is integrated for interactive documentation and testing.

- **Logging**  
  Serilog is used for structured logging, with logs written to file for diagnostics and auditing.

---

## 🛠️ Extending the Project

In this project, we have made it easy to extend functionality:
- New endpoints can be added by creating new controllers and services.
- Additional data sources can be supported by implementing new repository classes.
- Security can be enhanced by extending JWT claims and roles.
- Performance can be optimized by tuning caching strategies.

---

## 📚 Summary

In summary, this project demonstrates a robust, modular, and secure approach to building a modern .NET API. The design choices support scalability, maintainability, and ease of future enhancements.






# 🍺 elfBeautyBrewery API Documentation  

## 📌 Overview
The **elfBeautyBrewery API** allows you to authenticate and interact with brewery data.  
All endpoints are versioned using the path parameter `{apiVersion}` (e.g., `/v1/api/...`).  

- **Base URL:** `https://yourdomain.com/v{apiVersion}/api/`  
- **Authentication:** JWT Bearer Token (passed as `Authorization: Bearer <token>`)  
- **Content-Type:** `application/json`  

---

## 🚀 Quick Start

1. **Login to get a JWT token**
   ```bash
   curl -X POST "https://yourdomain.com/v1/api/Authentication/login"      -H "Content-Type: application/json"      -d '{
           "userName": "admin",
           "password": "password123"
         }'
   ```

2. **Copy the token** from the response.

3. **Use the token** in the `Authorization` header for all secured endpoints:
   ```bash
   curl -X GET "https://yourdomain.com/v1/api/Brewery/GetAll"      -H "accept: application/json"      -H "Authorization: Bearer <your_token>"
   ```

---

## 🔑 Authentication  

### Login  
Authenticate a user and receive a JWT token.  

```http
POST /v{apiVersion}/api/Authentication/login
```

**Path Parameters**  
| Name        | Type   | Required | Description   |
|-------------|--------|----------|---------------|
| `apiVersion` | string | ✅ | API version (e.g. `1`) |

**Request Body**  
```json
{
  "userName": "string",
  "password": "string"
}
```

**Response (200 OK)**  
Returns a JWT token (format depends on implementation).  

---

## 🍺 Brewery Endpoints  

### Get All Breweries  
Retrieve a list of all breweries.  

```http
GET /v{apiVersion}/api/Brewery/GetAll
```

**Path Parameters**  
| Name        | Type   | Required | Description |
|-------------|--------|----------|-------------|
| `apiVersion` | string | ✅ | API version |

**Response (200 OK)**  
```json
[
  {
    "id": "b54b16e1-ac3b-4bff-a11f-f7ae9ddc27e0",
    "name": "MadTree Brewing 2.0",
    "brewery_type": "regional",
    "city": "Cincinnati",
    "state": "Ohio",
    "country": "United States"
  }
]
```

---

### Get Brewery by Id  
Retrieve details of a specific brewery.  

```http
GET /v{apiVersion}/api/Brewery/Get?id={id}
```

**Path Parameters**  
| Name        | Type   | Required | Description |
|-------------|--------|----------|-------------|
| `apiVersion` | string | ✅ | API version |

**Query Parameters**  
| Name | Type | Required | Description |
|------|------|----------|-------------|
| `id` | uuid | ✅ | Brewery unique identifier |

**Response (200 OK)**  
```json
{
  "id": "b54b16e1-ac3b-4bff-a11f-f7ae9ddc27e0",
  "name": "MadTree Brewing 2.0",
  "brewery_type": "regional",
  "address_1": "5164 Kennedy Ave",
  "city": "Cincinnati",
  "state": "Ohio",
  "postal_code": "45213",
  "country": "United States"
}
```

---

### Search Breweries  
Search breweries by keyword with sorting options.  

```http
POST /v{apiVersion}/api/Brewery/Search
```

**Path Parameters**  
| Name        | Type   | Required | Description |
|-------------|--------|----------|-------------|
| `apiVersion` | string | ✅ | API version |

**Request Body**  
```json
{
  "search": "Ale",
  "sortColumn": "name",
  "sortOrder": "asc"
}
```

**Response (200 OK)**  
```json
[
  {
    "id": "123e4567-e89b-12d3-a456-426614174000",
    "name": "AleSmith Brewing Company",
    "brewery_type": "micro",
    "city": "San Diego",
    "country": "United States"
  }
]
```

---

## ⚠️ Error Handling
| Code | Meaning | When it happens |
|------|---------|-----------------|
| 400  | Bad Request | Invalid parameters |
| 401  | Unauthorized | Missing or invalid token |
| 404  | Not Found | Brewery not found |
| 500  | Internal Server Error | Unexpected issue |

---

## 💡 Usage Examples  

**Login (Get JWT token):**  
```bash
curl -X POST "https://yourdomain.com/v1/api/Authentication/login"   -H "Content-Type: application/json"   -d '{
        "userName": "admin",
        "password": "password123"
      }'
```

**Get All Breweries:**  
```bash
curl -X GET "https://yourdomain.com/v1/api/Brewery/GetAll"   -H "accept: application/json"   -H "Authorization: Bearer <your_token>"
```

**Search Breweries:**  
```bash
curl -X POST "https://yourdomain.com/v1/api/Brewery/Search"   -H "accept: application/json"   -H "Authorization: Bearer <your_token>"   -H "Content-Type: application/json"   -d '{
        "search": "Lager",
        "sortColumn": "name",
        "sortOrder": "desc"
      }'
```
