import json
import requests
import os

def login_lemonsoft():
    """
    Logs into the Lemonsoft system using environment variables.

    This function reads the API_URL, API_USERNAME, API_PASSWORD, API_DATABASE, and API_APIKEY from the environment variables.
    It then sends a POST request to the login URL with these details in the payload.

    Returns:
        str: The session ID if the login is successful, None otherwise.
    """
    API_URL = os.environ.get("API_URL")
    API_USERNAME = os.environ.get("API_USERNAME")
    API_PASSWORD = os.environ.get("API_PASSWORD")
    API_DATABASE = os.environ.get("API_DATABASE")
    API_APIKEY = os.environ.get("API_APIKEY")

    login_url = f"{API_URL}/api/auth/login"

    payload = {
        "username": API_USERNAME,
        "password": API_PASSWORD,
        "database": API_DATABASE,
        "apikey": API_APIKEY,
    }

    headers = {'Content-Type': 'application/json'}

    response = requests.post(login_url, headers=headers, data=json.dumps(payload))

    if response.status_code == 200:
        r = response.json()
        return r['session_id']

    return None

def lemonsoft_get_customers(session_id):
    """
    Fetches all customers from the Lemonsoft system using the provided session ID.

    This function sends a GET request to the Lemonsoft API's customers endpoint, paginating through the results until no more customers are returned.

    Args:
        session_id (str): The session ID obtained from a successful login.

    Returns:
        list: A list of customer data in dictionary format.

    Raises:
        Exception: If the GET request returns a status code other than 200.
    """
    API_URL = os.environ.get("API_URL")
    headers = {'Content-Type': 'application/json', 'Session-Id': session_id}
    customers = []
    page = 1

    while True:
        url = f"{API_URL}/api/customers?page={page}"
        response = requests.get(url, headers=headers)

        if response.status_code != 200:
            break

        r = response.json()
        if r['result_count'] == 0:
            break

        customers.extend(r['results'])
        page += 1

    return customers

if __name__ == "__main__":
    session_id = login_lemonsoft()
    if session_id:
        print(f"Login successful. Session ID: {session_id}")
    else:
        print("Login failed.")

    customers = lemonsoft_get_customers(session_id)
    print(customers)

    
    
