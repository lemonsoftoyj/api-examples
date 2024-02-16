# Lemonsoft API Client

This is a simple PHP script that logs into the Lemonsoft system and fetches all customers.

## Requirements

- PHP 7.4 or higher
- Composer (for managing dependencies)

## Installation

1. Clone this repository to your local machine.
2. Navigate to the project directory.
3. Install the dependencies with Composer:

```bash
composer install
```

## Configuration

This script uses environment variables for configuration. You need to set the following variables:

API_URL=your_api_url
API_USERNAME=your_username
API_PASSWORD=your_password
API_DATABASE=your_database
API_APIKEY=your_api_key

Replace `your_api_url`, `your_username`, `your_password`, `your_database`, and `your_api_key` with your Lemonsoft API credentials.

## Running the Script

To run the script, navigate to the project directory in your terminal and run:

```bash
php get_customers.php
```

The script will print the session ID if the login is successful, and then it will print the list of customers.

Please note that this README assumes you have PHP and Composer installed on your machine. If you don't, you can download PHP from [php.net](https://www.php.net/downloads) and Composer from [getcomposer.org](https://getcomposer.org/download/).

