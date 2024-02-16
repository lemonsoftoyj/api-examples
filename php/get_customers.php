<?php

/**
 * Logs into the Lemonsoft system using environment variables.
 *
 * This function reads the API_URL, API_USERNAME, API_PASSWORD, API_DATABASE, and API_APIKEY from the environment variables.
 * It then sends a POST request to the login URL with these details in the payload.
 *
 * @return string|null The session ID if the login is successful, null otherwise.
 */
function login_lemonsoft() {
    $API_URL = getenv("API_URL");
    $API_USERNAME = getenv("API_USERNAME");
    $API_PASSWORD = getenv("API_PASSWORD");
    $API_DATABASE = getenv("API_DATABASE");
    $API_APIKEY = getenv("API_APIKEY");

    $login_url = "{$API_URL}/api/auth/login";

    $payload = array(
        "username" => $API_USERNAME,
        "password" => $API_PASSWORD,
        "database" => $API_DATABASE,
        "apikey" => $API_APIKEY,
    );

    $headers = array('Content-Type: application/json');

    $context = stream_context_create(array(
        'http' => array(
            'method' => 'POST',
            'header' => $headers,
            'content' => json_encode($payload),
        )
    ));

    $response = file_get_contents($login_url, false, $context);

    if ($http_response_header[0] == "HTTP/1.1 200 OK") {
        $r = json_decode($response, true);
        return $r['session_id'];
    }

    return null;
}

/**
 * Fetches all customers from the Lemonsoft system using the provided session ID.
 *
 * This function sends a GET request to the Lemonsoft API's customers endpoint, paginating through the results until no more customers are returned.
 *
 * @param string $session_id The session ID obtained from a successful login.
 * @return array A list of customer data in dictionary format.
 */
function lemonsoft_get_customers($session_id) {
    $API_URL = getenv("API_URL");
    $headers = array('Content-Type: application/json', "Session-Id: {$session_id}");
    $customers = array();
    $page = 1;

    do {
        $url = "{$API_URL}/api/customers?page={$page}";

        $context = stream_context_create(array(
            'http' => array(
                'method' => 'GET',
                'header' => $headers,
            )
        ));

        $response = file_get_contents($url, false, $context);

        if ($http_response_header[0] != "HTTP/1.1 200 OK") {
            break;
        }

        $r = json_decode($response, true);

        if (isset($r['results'])) {
            array_push($customers, ...$r['results']);
        }

        $page += 1;
    } while (isset($r['result_count']) && $r['result_count'] > 0);

    return $customers;
}

$session_id = login_lemonsoft();
if ($session_id) {
    echo "Login successful. Session ID: {$session_id}\n";
} else {
    echo "Login failed.\n";
}

$customers = lemonsoft_get_customers($session_id);
print_r($customers);
