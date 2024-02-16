# Lemonsoft Client

This project contains a simple client for interacting with the Lemonsoft API. It logs into the Lemonsoft system using environment variables and fetches all customers from the system.

## Prerequisites

- .NET Core 3.1 or later
- Environment variables for the Lemonsoft API:
  - `API_URL`: The base URL of the Lemonsoft API.
  - `API_USERNAME`: The username for the Lemonsoft API.
  - `API_PASSWORD`: The password for the Lemonsoft API.
  - `API_DATABASE`: The database for the Lemonsoft API.
  - `API_APIKEY`: The API key for the Lemonsoft API.

## Running the Code

1. Clone the repository:
    ```bash
    git clone https://github.com/lemonsoftoyj/api-examples.git
    ```
2. Navigate to the project directory:
    ```bash
    cd yourrepository
    ```
3. Run the code:
    ```bash
    dotnet run
    ```


## What the Code Does

The `LemonsoftClient` class logs into the Lemonsoft system and fetches all customers from the system.

The `LoginAsync` method logs into the Lemonsoft system using the provided environment variables and returns the session ID.

The `GetCustomersAsync` method fetches all customers from the Lemonsoft system using the provided session ID and returns a list of customer data.

The `CreateCustomerUrl` method creates the URL for fetching customers.

The `ApiResponse` class represents the API response.

The `Program` class contains the `Main` method, which creates a new `LemonsoftClient`, logs into the Lemonsoft system, and fetches all customers from the system.
