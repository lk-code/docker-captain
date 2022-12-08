# Notes for the development

## Database

### .NET Core Entity Framework

#### Migrations
Changes to the database must be saved via migrations.
These are then executed automatically when the app is started.

##### Create migration
 1. open Terminal-App
 2. change to the project directory of the database project (where the *.csproj of the EFCore project is located)
 3. run "dotnet tool install --global dotnet-ef" to make sure that the dotnet ef tools are installed
 4. run "dotnet ef migrations add {short version of the change}" => e.g. "dotnet ef migrations add InitialCreate".
 5. this should create a new *.cs file in the project under /Migrations.