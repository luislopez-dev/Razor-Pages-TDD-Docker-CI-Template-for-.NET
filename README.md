# **Enterprise Template for apps in .NET**
# <sub> Powered by Selenium, CA Principles, TDD, Docker & GitHub Actions </sub>

## Introduction

This project provides a solid foundation for building web applications with integrated DevOps tools out of the box. Its goal is to provide a clear and maintainable starting point that can grow without complications. It is designed for teams or individual developers who want to begin with a well-defined structure without having to reinvent essential configurations.

## Structure

<img width="502" height="666" alt="Project (1)" src="https://github.com/user-attachments/assets/7c53569d-2bdc-4fcb-b59a-54e94083838d" />

### Domain

<strong>Contains the core business rules.</strong>

The adoption of DDD principles for this layer is currently being evaluated for a potential update.

### Application

<strong>Defines the application services and coordinates communication between the presentation layer and the business logic.</strong>

The services in this project might be refactored into individual use cases, 
and the whole layer might be refactored with a Vertical-Slice approach in future updates.

Integrating DTOs is on the roadmap for a future release.

### Infrastructure

<strong>Implements technical details such as data access and external services.</strong>

The repositories in this template could be refactored through a generic repository in the future.

### Presentation

Contains the Razor Pages and <strong> the logic that handles user interaction.</strong>


## Business Logic

This project emulates an inventory and billing management system.

## Development Approach
The solution follows a TDD-oriented workflow, incorporating unit and integration tests to validate key components and ensure code quality from the early stages of development. In addition, end-to-end tests are implemented using Selenium to verify critical user flows in a real browser environment.

## Continuous Integration
The repository includes a CI pipeline built with GitHub Actions. This workflow automates test execution and verifies that every change meets the defined standards. 

## Runtime Environment
To simplify setup and ensure consistency across environments, the project uses Docker Compose. The configuration includes support for SQL Server, allowing you to easily replicate a realistic database environment for both development and testing.

## YouTube Video
For a short demonstration, I recorded the following video:

[Watch on YouTube](https://youtu.be/0nfXpb7OsPA?si=28_t2m6mDIMfSiVw)

## Project created by [Luis López](https://github.com/luislopez-dev)
