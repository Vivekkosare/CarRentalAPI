var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.CarRentalAPI>("carrentalapi");

builder.Build().Run();
