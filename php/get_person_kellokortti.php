<?php
function get_kellokortti_person($person_id) {
    $apiUrl = getenv('KK_API_URL') ?: 'https://api.kellokortti.fi/api';
    $username = getenv('KK_USERNAME');
    $password = getenv('KK_PASSWORD');
    if (!$username || !$password) {
        throw new Exception('KK_USERNAME and KK_PASSWORD must be set');
    }
    $url = "$apiUrl/api/v1/person/$person_id";
    $opts = array(
        'http' => array(
            'method' => 'GET',
            'header' => 'Authorization: Basic ' . base64_encode("$username:$password")
        )
    );
    $context = stream_context_create($opts);
    $response = file_get_contents($url, false, $context);
    return json_decode($response, true);
}

$pid = getenv('KK_PERSON_ID') ?: 'testuser';
print_r(get_kellokortti_person($pid));
?>
