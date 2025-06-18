using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class KellokorttiClient
{
    private static readonly HttpClient client = new HttpClient();
    private readonly string apiUrl;
    private readonly string username;
    private readonly string password;

    public KellokorttiClient()
    {
        apiUrl = Environment.GetEnvironmentVariable("KK_API_URL") ?? "https://api.kellokortti.fi/api";
        username = Environment.GetEnvironmentVariable("KK_USERNAME");
        password = Environment.GetEnvironmentVariable("KK_PASSWORD");
    }

    public async Task<dynamic> GetPersonAsync(string personId)
    {
        var url = $"{apiUrl}/api/v1/person/{personId}";
        var credentials = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"{username}:{password}"));
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);
        var response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<dynamic>(content);
    }
}

public class Program
{
    public static async Task Main(string[] args)
    {
        var client = new KellokorttiClient();
        var personId = Environment.GetEnvironmentVariable("KK_PERSON_ID") ?? "testuser";
        var person = await client.GetPersonAsync(personId);
        Console.WriteLine(JsonConvert.SerializeObject(person));
    }
}
