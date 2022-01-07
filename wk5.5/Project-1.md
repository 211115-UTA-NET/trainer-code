# project 1: store web application
Nov 15 2021 Arlington .NET / Richard Hawkins, Nick Escalona

## functionality
* interactive console application with a REST HTTP API backend
* place orders to store locations for customers
* add a new customer
* search customers by name
* display details of an order
* display all order history of a store location
* display all order history of a customer
* input validation (in the console app and also in the server)
* exception handling, including foreseen SQL and HTTP errors
* persistent data; no prices, customers, order history, etc. hardcoded in C#
* (recommended: asynchronous network & other I/O, at least on the REST API)
* (optional: logging of exceptions and other events
* (optional: order history can be sorted by earliest, latest, cheapest, most expensive)
* (optional: get a suggested order for a customer based on his order history)
* (optional: display some statistics based on order history)

## design
* use ADO.NET (not Entity Framework)
* use ASP.NET Core Web API
* use an Azure SQL DB in third normal form
* have a SQL script that can set up the database from scratch
* don't use public fields
* define and use at least one interface
* best practices: separation of concerns, OOP principles, SOLID, REST, HTTP
* XML documentation

### REST API

* the API should own the business logic of the application, not just the data access logic
* it shouldn't trust that the console app hasn't been tampered with
* should be able to handle multiple instances of the console app connecting to it at the same time
* use dependency injection for controller dependencies
* separate different concerns into different classes
* use repository pattern for data access
* recommended to keep the Web API project for only HTTP input/output concerns
* recommended to use separate classes to help validate/format the HTTP message bodies (DTOs for model binding and action results)
* recommended to separate business logic into a separate project from the Web API project and any HTTP or ADO.NET concerns
* recommended to separate the data access into a separate project too

#### customer
* has first name, last name, etc.
* (optional: has a default store location to order from)

#### order
* has a store location
* has a customer
* has an order time (when the order was placed)
* can contain multiple kinds of product in the same order
* rejects orders with unreasonably high product quantities
* (optional: some additional business rules, like special deals)

#### location
* has an inventory
* inventory decreases when orders are accepted
* rejects orders that cannot be fulfilled with remaining inventory
* (optional: for at least one product, more than one inventory item decrements when ordering that product)

#### product (etc.)

### console app
* the console app provides a UI, interprets user input, uses the REST API over HTTP, and formats output
* should gracefully handle HTTP error codes from the server, as well as connection errors
* separate different concerns into different classes
* recommended to separate the connection to the API into a separate project
* recommended to keep the console app project for only console interface concerns, not HTTP concerns

### tests
* at least 10 test methods
* at least 1 test should use Moq
* no tests should connect to the app's actual database

### CI/CD
* your console app should include a CI pipeline to analyze with SonarCloud and perform any unit tests you have written
* your console app should include a CD pipeline to build, publish, and create a Docker image of your app, and push it to your DockerHub repo
* your API should include a CI pipeline to analyze with SonarCloud and perform any unit tests you have written
* your API should include a CD pipeline to build, publish, and deploy your app to Azure App Service for deployment
