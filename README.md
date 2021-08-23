## Web Api Template

Various Visual Studio 2019 Project Templates
  * No Authentication
  * JWT Authentication
  * Gateway with JWT Authentication

All Project templates out of the box contain:
  * API (Controllers), BLL (Providers) and DAL (Repositories) structure with constructor DI
  * Custom Exception Handling middleware
  * Swagger-Swashbuckle OpenAPI documentation
  * Serilog File and Console (file configured) logging

#### Usage

First open the .sln file of the type of Project Template you want to use (with or without auth, etc.) in Visual Studio:
  1. Click the "Project" tab
  2. Choose "Export Template"
  3. Choose "Project Template" and press "Next"
  4. Edit the "Template Name" and ensure the checkbox labelled "Automatically import the template in Visual Studio" is checked
  5. Press "Finish"

Then search for the "Template Name" when creating a new Project in Visual Studio


