# MOJTechTest

Open .sln in Visual Studio
Ensure the db connection string in appsettings.json is correct
In Package manager console run 
  dotnet ef migrations add InitialCreate --project ./MOJTechTest
  dotnet ef database update
In Visual Studio click run

In MOJTechTest\client-app run
  npm install
  npm start

To run C# unit tests, in visual studio in the top menu, click Test, Run all tests

There is no unit tests or validation in the front-end react application
