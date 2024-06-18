Clean Architecture CosmosDB API

This project is a Clean Architecture implementation using ASP.NET Core and Azure Cosmos DB. It provides a RESTful API for managing programs and user authentication. The project demonstrates the use of AutoMapper, JWT for authentication, and a repository pattern for data access.


Getting Started

Prerequisites

#.NET 6.0 SDK
#Azure Cosmos DB account
#Postman for API testing (optional)
#Installation

Clone the repository:
```````````````````
bash
Copy code
git clone https://github.com/your-repository/CleanArchitectureCosmosDb.git
cd CleanArchitectureCosmosDb
`````````````````````````````


Restore the dependencies:
`````````````````````````````
dotnet restore
`````````````````````

Configuration

#Rename appsettings.example.json to appsettings.json.

Update the appsettings.json file with your Cosmos DB and JWT settings:

````````````````````````
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "ConnectionStrings": {
    "CosmosDb": "AccountEndpoint=https://<your-cosmos-db-endpoint>;AccountKey=<your-account-key>;"
  },
  "JwtSettings": {
    "Secret": "your-secret-key",
    "Issuer": "your-issuer",
    "Audience": "your-audience",
    "ExpiryMinutes": 60
  },
  "CosmosDb": {
    "DatabaseName": "your-database-name"
  },
  "AllowedHosts": "*"
}
`````````````````````````````

Running the Application


Run the application:

`````````````````
dotnet run
`````````````````

Open your browser and navigate to https://localhost:5001/swagger to view the Swagger UI and explore the API.

Project Structure

The project follows the Clean Architecture principles, with a clear separation of concerns across different layers:

#Domain: Contains the core entities and interfaces.
#Application: Contains DTOs, services, and mapping profiles.
#Infrastructure: Contains the Cosmos DB repositories.
#API: Contains the controllers and configuration for the web API.
Endpoints
#Authentication
``````````
POST /api/Auth/Authenticate
Description: Authenticates a user and returns a JWT token.
Request Body:

````````````````````````````
{
  "Username": "string",
  "Password": "string"
}
Response:
json
Copy code
{
  "Token": "string",
  "Username": "string",
  "Role": "string"
}
````````````````````````````

Program Management

GET /api/Program/GetPrograms
Description: Gets all programs.
Response: 200 OK

GET /api/Program/GetProgramById/{id}
Description: Gets a program by ID.
Response: 200 OK or 404 Not Found

POST /api/Program/AddProgram
Description: Adds a new program.
Request Body:

```````````````````````````````
{
  "Id": "string",
  "Topic": "string",
  "Description": "string",
  "ApplicationForms": [ ... ]
}
``````````````````````````````

Response: 201 Created
PUT /api/Program/UpdateProgram/{id}
Description: Updates a program by ID.
Request Body:

```````````````````````````````
{
  "Id": "string",
  "Topic": "string",
  "Description": "string",
  "ApplicationForms": [ ... ]
}
```````````````````````````````````

Response: 204 No Content

DELETE /api/Program/DeleteProgram/{id}
Description: Deletes a program by ID.
Response: 204 No Content

Testing

Use Postman or any other API testing tool to test the endpoints. You can import the Swagger JSON file from https://localhost:5001/swagger/v1/swagger.json into Postman to create a collection of the API endpoints.

