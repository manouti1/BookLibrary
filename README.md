# Library Management API

## Objective

The goal of this project is to develop and deploy a RESTful API for managing a library of books using AWS Fargate, automate infrastructure provisioning with Terraform, and implement a CI/CD pipeline using GitHub Actions.

## Features

- **RESTful API Development** using .NET 8/9
- **Entity Framework Core** integration for database management
- **CRUD Operations** for managing books
- **JWT Authentication** for secure endpoints
- **CI/CD Pipeline** using GitHub Actions
- **Infrastructure Automation** with Terraform
- **API Documentation** using Swagger/OpenAPI

---

## Requirements

### 1. RESTful API Development

#### i. Project Setup

- Use **.NET 8/9** to create a Web API project for the Library Management System.
- Configure **Entity Framework Core** to work with a **SQLite** or **SQL Server** database for storing book details.

#### ii. Entity Framework Core

- Define a **Book** entity with the following properties:
  - `Id` (int): Unique identifier for each book.
  - `Title` (string): Title of the book.
  - `Author` (string): Author of the book.
  - `ISBN` (string): International Standard Book Number (ISBN).
  - `PublishedDate` (DateTime): The date when the book was published.

- Set up a **LibraryContext** for managing the database using Entity Framework Core.

- Implement **CRUD operations** for the Book entity:
  - **GET /api/books**: Retrieve all books from the database.
  - **GET /api/books/{id}**: Retrieve a book by its ID.
  - **POST /api/books**: Add a new book to the library.
  - **PUT /api/books/{id}**: Update an existing book by its ID.
  - **DELETE /api/books/{id}**: Delete a book by its ID.

---

### 2. Security

- Implement **JWT Authentication**:
  - Secure all CRUD endpoints for authenticated users only.
  - Add endpoints for **user registration** and **login** to generate JWT tokens.
  - Ensure that users must be authenticated to perform any CRUD operations on the books.

---

### 3. Validation and Error Handling

- **Input Validation**:
  - Ensure that **Title** and **Author** fields are non-empty and valid.
  
- **Error Handling**:
  - Implement proper error handling for invalid requests and server errors.
  - Return meaningful **HTTP status codes** and appropriate error messages.

---

### 4. Documentation

- Use **Swagger** for API documentation, or an **OpenAPI document**.
  - Include **example requests** and **responses** for each API endpoint.

---

## Setup Instructions

### 1. Clone the Repository

Clone the repository to your local machine:

```bash
git clone https://github.com/yourusername/library-api.git
cd library-api
```
## Run Migrations
dotnet ef migrations add InitialCreate
dotnet ef database update
