import os
import requests
from requests.auth import HTTPBasicAuth


def get_person(person_id: str):
    """Fetch a person from the Kellokortti API using Basic authentication."""
    api_url = os.environ.get("KK_API_URL", "https://api.kellokortti.fi/api")
    username = os.environ.get("KK_USERNAME")
    password = os.environ.get("KK_PASSWORD")
    if not username or not password:
        raise ValueError("KK_USERNAME and KK_PASSWORD must be set")

    url = f"{api_url}/api/v1/person/{person_id}"
    response = requests.get(url, auth=HTTPBasicAuth(username, password))
    response.raise_for_status()
    return response.json()


if __name__ == "__main__":
    pid = os.environ.get("KK_PERSON_ID", "testuser")
    print(get_person(pid))
