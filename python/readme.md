# Lemonsoft Customer Fetcher

This Python script logs into the Lemonsoft system and fetches all customers.

## Prerequisites

You need Python 3.6 or later to run this script. You can check your Python version by running `python --version`.

## Installation

1. Clone this repository to your local machine.
2. Install the required Python packages by running `pip install -r requirements.txt` in the project directory.

## Configuration

This script uses environment variables for configuration. You need to set the following variables:

- `API_URL`: The URL of the Lemonsoft API.
- `API_USERNAME`: Your Lemonsoft API username.
- `API_PASSWORD`: Your Lemonsoft API password.
- `API_DATABASE`: Your Lemonsoft API database.
- `API_APIKEY`: Your Lemonsoft API key.

You can set these variables in your shell, or you can use a `.env` file if you're using a package like `python-dotenv`.

## Running the Script

To run the script, navigate to the project directory in your terminal and run `python get_customers.py`.

The script will print the session ID if the login is successful, and then it will print the list of customers.
