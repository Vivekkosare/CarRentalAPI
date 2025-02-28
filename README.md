# Car Rental API

This is a car rental API that allows you to book a car, and register pick-up and return details. 

## Tech Stack

- C#
- .NET 8.0
- PostgreSQL database

## Local Setup

### 1. Install PostgreSQL Locally

Make sure you have PostgreSQL installed locally. You can download and install it from the official PostgreSQL website:  
[PostgreSQL Download](https://www.postgresql.org/download/)

### 2. Set Up Database

Once PostgreSQL is installed, create a new database for the API (e.g., `CarRental`) and update the connection string in the appsettings.json file of CarRentalAPI project.

### 3. Run Database Migrations

After setting up the database, run the following commands in the terminal to apply migrations:

1. **Add Initial Migration**

    Run the following command to create the initial migration:

    ```bash
    dotnet ef migrations add InitialCreate
    ```

2. **Update Database**

    Apply the migration to your database:

    ```bash
    dotnet ef database update
    ```

3. **Add Additional Migration**

    If you have further schema changes (e.g., adding a new column), you can add additional migrations. For example, to add `CurrentMeterReading` to the `Car` table, run:

    ```bash
    dotnet ef migrations add AddCurrentMeterReadingToCar
    ```

4. **Update Database Again**

    Apply this new migration to the database:

    ```bash
    dotnet ef database update
    ```

### 4. Verify Database and Tables

After running the migrations, you can verify that the tables were created successfully by connecting to your PostgreSQL database:

```bash
psql -U postgres -d CarRental
