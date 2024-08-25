## Getting Started
The following prerequisites are required to build and run the solution:
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) (latest version)
- [Node.js](https://nodejs.org/) (latest LTS, for Angular 18)
- [SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

Clone the repo from master branch to your machine.

## Database
You can use SSMS to connect to `(localdb)\\mssqllocaldb` to make sure it was installed correctly

## API
Launch the api:
```bash
cd phtb\api\PHTB\Api
dotnet run
```
If it build and run succesfully, you can check the following:
* The database PHTB is created.
* Open the swagger via link: https://localhost:7109/swagger/index.html (the port is defined in `launchSettings.json` file)

## UI
Launch the angular app:
```bash
cd phtb\ui\phtb
npm install
npm start
```
If it build and run succesfully, you can check by the link http://localhost:4200/

## Unit Tests
Run the unit tests for API
```bash
cd phtb\api\PHTB
dotnet test
```
