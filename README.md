**Languages:**  
[🇬🇹 Español](README.es.md) | [🌎 English](README.md)

## **Enterprise Template for apps in .NET and Razor Pages** - Powered by Selenium, Clean Architecture, TDD, Docker & Github Actions

## Table of contents

## Instroduction

This project provides a solid foundation for building web applications with Razor Pages. Its goal is to provide a clear and maintainable starting point that can grow without complications. It is designed for teams or individual developers who want to begin with a well-defined structure without having to reinvent essential configurations.

## Structure

**Business**: Contains the core domain rules.

**Application**: Defines the application services and coordinates communication between the presentation layer and the business logic.

**Infrastructure**: Implements technical details such as data access and external services.

**Presentation**: Contains the Razor Pages and the logic that handles user interaction.

<img width="578" height="404" alt="520527297-0764a3f9-4737-4d57-aae4-deaf11605441" src="https://github.com/user-attachments/assets/43d80601-16a6-4d89-8933-d0ae8cf0a267" />

## Development Approach
The solution follows a TDD-oriented workflow, incorporating unit and integration tests to validate key components and ensure code quality from the early stages of development. In addition, end-to-end tests are implemented using Selenium to verify critical user flows in a real browser environment. This approach helps support safer, more predictable, and reliable development cycles.

## Continuous Integration
The repository includes a CI pipeline built with GitHub Actions. This workflow automates test execution and verifies that every change meets the defined standards. 

## Runtime Environment
To simplify setup and ensure consistency across environments, the project uses Docker Compose. The configuration includes support for SQL Server, allowing you to easily replicate a realistic database environment for both development and testing.

## YouTube Video
For a short demonstration, I recorded the following video:

[Watch on YouTube](https://youtu.be/0nfXpb7OsPA?si=28_t2m6mDIMfSiVw)

## Project created and maintained by [Luis López](https://github.com/luislopez-dev)
