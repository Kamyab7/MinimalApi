# MinimalApi

[![CI](https://github.com/mehdihadeli/store-golang-microservices/actions/workflows/ci.yml/badge.svg?branch=main&style=flat-square)](https://github.com/Kamyab7/MinimalApi/blob/main/.github/workflows/build.yml)

🌀 This Application is `in-progress` and I will add new features and thecnologies over time. 

## Technologies - Libraries

- ✔️ **[`jbogard/MediatR`](https://github.com/jbogard/MediatR)** - Simple, unambitious mediator implementation in .NET
- ✔️ **[`RicoSuter/NSwag`](https://github.com/RicoSuter/NSwag)** - The Swagger/OpenAPI toolchain for .NET, ASP.NET Core and TypeScript.
- ✔️ **[`angular/angular`](https://github.com/angular/angular)** - The modern web developer’s platform

## Plan
> This project is in progress, New features will be added over time.
High-level plan is represented in the table

| Feature | Status |
| ------- | ------ |
| Client Application | In Progress 👷‍ |
| Clean Architecture | Done ✔️ |
| API Endpoints | Done ✔️ |
| Security | Not Started 🚩 |

## Application Structure

I used [CQRS](https://www.eventecommerce.com/cqrs-pattern) for decompose my features to very small parts that makes our application:

- maximize performance, scalability and simplicity.
- adding new feature to this mechanism is very easy without any breaking change in other part of our codes. New features only add code, we're not changing shared code and worrying about side effects.
- easy to maintain and any changes only affect on one command or query (or a slice) and avoid any breaking changes on other parts
- it gives us better separation of concerns and cross cutting concern (with help of MediatR behavior pipelines) in our code instead of a big service class for doing a lot of things.

With using [CQRS](https://event-driven.io/en/cqrs_facts_and_myths_explained/), our code will be more aligned with [SOLID principles](https://en.wikipedia.org/wiki/SOLID), especially with:

- [Single Responsibility](https://en.wikipedia.org/wiki/Single-responsibility_principle) rule - because logic responsible for a given operation is enclosed in its own type.
- [Open-Closed](https://en.wikipedia.org/wiki/Open%E2%80%93closed_principle) rule - because to add new operation you don’t need to edit any of the existing types, instead you need to add a new file with a new type representing that operation.

Usually, when we work on a given functionality we need some technical things for example:

- API endpoint
- Request Input
- Request Output
- Some class to handle Request, For example Command and Command Handler or Query and Query Handler
- Data Model
