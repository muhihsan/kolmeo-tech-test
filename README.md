# Kolmeo Tech Test

This repository contains a simple Product CRUD API.

## Getting Started

### Requirements
- dotnet -> [Download dotnet](https://dotnet.microsoft.com/en-us/download)

### Build

To build the project, navigate to the `src/` directory and run the following command:

```shell
dotnet build
```

### Test

To run the tests, navigate to the `src/` directory and execute the following command:

```shell
dotnet test
```

### Run

To run the application, navigate to the `src/Product.API/` directory and run the following command:

```shell
dotnet run
```

### Swagger Endpoint

Access the Swagger API documentation and test the endpoints using the following URL:

```shell
https://localhost:7010/swagger/index.html
```

## Description

The following assumptions were made during the development of this application:

- This is a simple CRUD API, so using a more complex architecture like Clean Architecture is unnecessary.
- No authentication (token authentication) is required for this API.
- Using EF Core InMemory database is sufficient for the current needs.
- Publishing an event when the model is updated is not necessary.
- The application is designed with a structure that facilitates easy testing, using the repository pattern.
- The addition of a UI for testing the API during development is beneficial, hence Swagger is integrated.
- The code contains many TODO comments indicating potential future improvements.
- Comments have been added to the code to explain certain decisions made during development.