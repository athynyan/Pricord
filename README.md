# Pricord

Pricord is a full stack web application used to record team compositions for the game Princess Connect Re:Dive.

The application is built using the following technologies:
- ASP.NET 7.0
- Blazor Server
- Entity Framework Core
- PostgreSQL

It follows the Domain Driven Design (DDD) architecture pattern. The application is split into the following projects:
- Pricord.Domain - Contains the domain entities and business logic
- Pricord.Application - Contains the application services, validations, abstractions and behaviours
- Pricord.Web - Contains the Blazor components and pages for the web application
- Pricord.Contracts - Contains the API contracts and data transfer objects to be shared between the API and Blazor
- Pricord.API - Contains the API controllers, mapping logic between the domain and contracts
- Pricord.Infrastructure - Contains the database context, repositories and external services

The project is currently in active development and is not yet deployed.