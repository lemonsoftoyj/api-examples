using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

public class LemonsoftClient
{
    private static readonly HttpClient client = new HttpClient();
    private string apiUrl;
    private string username;
    private string password;
    private string database;
    private string apiKey;

    public LemonsoftClient()
    {
        apiUrl = Environment.GetEnvironmentVariable("API_URL");
        username = Environment.GetEnvironmentVariable("API_USERNAME");
        password = Environment.GetEnvironmentVariable("API_PASSWORD");
        database = Environment.GetEnvironmentVariable("API_DATABASE");
        apiKey = Environment.GetEnvironmentVariable("API_APIKEY");
    }

    /// <summary>
    /// Asynchronously logs into the Lemonsoft system using environment variables.
    /// </summary>
    /// <returns>
    /// A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.
    /// The task result contains the session ID if the login is successful, null otherwise.
    /// </returns>
    public async Task<string> LoginAsync()
    {
        // Construct the login URL
        var loginUrl = $"{apiUrl}/api/auth/login";

        // Create the payload
        var payload = new
        {
            username = this.username,
            password = this.password,
            database = this.database,
            apikey = this.apiKey
        };

        // Convert the payload to a JSON string
        var payloadJson = JsonConvert.SerializeObject(payload);

        // Create the HTTP content
        var content = new StringContent(payloadJson, Encoding.UTF8, "application/json");

        // Send the POST request
        var response = await client.PostAsync(loginUrl, content);

        // Throw an exception if the HTTP response status does not indicate success
        response.EnsureSuccessStatusCode();

        // Read the response content as a string
        var responseContent = await response.Content.ReadAsStringAsync();

        // Deserialize the response content into a dynamic object
        var jsonResponse = JsonConvert.DeserializeObject<dynamic>(responseContent);

        // Return the session ID
        return jsonResponse.session_id;
    }

    /// <summary>
    /// Fetches all customers from the Lemonsoft system using the provided session ID.
    /// </summary>
    /// <param name="sessionId">The session ID obtained from a successful login.</param>
    /// <returns>A list of customer data in dictionary format.</returns>
    public async Task<List<dynamic>> GetCustomersAsync(string sessionId)
    {
        client.DefaultRequestHeaders.Add("Session-Id", sessionId);
        var customers = new List<dynamic>();
        var page = 1;

        while (true)
        {
            var url = CreateCustomerUrl(page);
            var response = await client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                break;

            var responseContent = await response.Content.ReadAsStringAsync();
            var jsonResponse = JsonConvert.DeserializeObject<ApiResponse>(responseContent);

            if (jsonResponse.ResultCount == 0)
                break;

            customers.AddRange(jsonResponse.Results);
            page++;
        }

        return customers;
    }

    /// <summary>
    /// Creates the URL for fetching customers.
    /// </summary>
    /// <param name="page">The page number.</param>
    /// <returns>The created URL.</returns>
    private string CreateCustomerUrl(int page)
    {
        return $"{apiUrl}/api/customers?page={page}";
    }

    /// <summary>
    /// Represents the API response.
    /// </summary>
    public class ApiResponse
    {
        [JsonProperty("result_count")]
        public int ResultCount { get; set; }

        public List<dynamic> Results { get; set; }
    }
}

public class Program
{
    public static async Task Main(string[] args)
    {
        var client = new LemonsoftClient();
        var sessionId = await client.LoginAsync();

        if (sessionId != null)
        {
            Console.WriteLine($"Login successful. Session ID: {sessionId}");
            var customers = await client.GetCustomersAsync(sessionId);
            Console.WriteLine(JsonConvert.SerializeObject(customers));
        }
        else
        {
            Console.WriteLine("Login failed.");
        }
    }
}
