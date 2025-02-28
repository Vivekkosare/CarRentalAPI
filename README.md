This is a car rental API, where you can Book a car, register pick and return.
The stack used for this is C#, .Net 8.0 and Postgresql database

Need to install postgresql db locally..as of now docker setup is not done..
Once the db is installed,
Run these commands

dotnet ef migrations add InitialCreate
dotnet ef database update

dotnet ef migrations add AddCurrentMeterReadingToCar
dotnet ef database update



