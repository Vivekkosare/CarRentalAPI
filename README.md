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

1. **To apply all migrations**

    Run the following command to apply the database migrations:

    ```bash
    dotnet ef database update
    ```

### 4. Verify Database and Tables

After running the migrations, you can verify that the tables were created successfully by connecting to your PostgreSQL database:

```bash
psql -U postgres -d CarRental
