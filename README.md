# Library Application (Web API Project)


![TypeScript](https://img.shields.io/badge/TypeScript-v.4-green)
![SASS](https://img.shields.io/badge/SASS-v.4-hotpink)
![React](https://img.shields.io/badge/React-v.18-blue)
![Redux toolkit](https://img.shields.io/badge/Redux-v.1.9-brown)
![.NET Core](https://img.shields.io/badge/.NET%20Core-v.7-purple)
![EF Core](https://img.shields.io/badge/EF%20Core-v.7-cyan)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-v.14-drakblue)

This project involves creating a Fullstack project with React and Redux on the frontend and ASP.NET Core 7 on the backend. The goal is to provide a seamless experience for users, along with robust management system for administrators.

- Frontend: SASS, MUI, TypeScript, React, Redux Toolkit
- Backend: ASP .NET Core, Entity Framework Core, PostgreSQL

This is a Web API project that provides Library's functionality, user management, and loan operations.

## Table of Contents

- [Introduction](#introduction)
- [Getting Started](#getting-started)
- [API Endpoints](#api-endpoints)
- [Authentication](#authentication)
- [Usage Examples](#usage-examples)
- [Contributing](#contributing)
- [License](#license)

## Introduction

This Web API project is designed to provide various services related to books, user management, and loan operations. It includes services to manage books, user accounts, carts, loans, and more.

## Getting Started

1. Clone this repository to your local machine.
2. Configure the connection string to your database in the `appsettings.example.json` file.
3. Run the application using your preferred development environment.

## API Endpoints

### Book Operations

- `GET /api/v1/Books`: Get a list of all books.
- `GET /api/v1/Books/{id}`: Get details of a specific book by ID.
- `POST /api/v1/Books`: Add a new book.
- `PUT /api/v1/Books/{id}`: Update details of a specific book by ID.
- `DELETE /api/v1/Books/{id}`: Delete a book by ID.

### User Operations

- `GET /api/v1/Users`: Get a list of all users (AdminOnly).
- `GET /api/v1/Users/{id}`: Get details of a specific user by ID (AdminOnly).
- `POST /api/v1/Users`: Create a new user.
- `PUT /api/v1/Users/{id}`: Update details of a specific user by ID.
- `DELETE /api/v1/Users/{id}`: Delete a user by ID.

### Loan Operations

- `GET /api/v1/Loans`: Get a list of all loans (AdminOnly).
- `GET /api/v1/Loans/user/{userId}`: Get loans associated with a specific user.
- `POST /api/v1/Loans`: Place a new loan.
- `PUT /api/v1/Loans/return/{loanId}`: Return a loan.

Please refer to the API documentation or your development environment for more details on available endpoints and their request/response formats.

## Authentication

Authentication is required to access certain endpoints. You can obtain an access token by following the authentication process specific to your project.

## Usage Examples

### Place a Loan

To place a loan, send a `POST` request to `/api/v1/Loans` with the loan details in the request body.

### Return a Loan

To return a loan, send a `PUT` request to `/api/v1/Loans/return/{loanId}` where `{loanId}` is the ID of the loan to be returned.

## Contributing

Contributions to this project are welcome. Please fork the repository, create a new branch for your changes, and submit a pull request.

## License

This project is licensed under the [MIT License](LICENSE).
