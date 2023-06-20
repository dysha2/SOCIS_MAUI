using SOCIS_MAUI_MODEL.Model;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;


HttpClient httpClient = new HttpClient();
//httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoiR2FyaWV2RGVuaXMiLCJyb2xlIjoiYWRtaW4iLCJJZCI6IjI5IiwibmJmIjoxNjg2MDM2NjUyLCJleHAiOjE2ODYwNzc5NDAsImlhdCI6MTY4NjAzNjY1MiwiaXNzIjoiRUNUUyIsImF1ZCI6IlNPQ0lTIn0.S_DCnWjdaKG-vHpiyCWOezO_9xrGDs_3UP3zExaxUU0");
httpClient.BaseAddress = new Uri("https://localhost:5001/");
HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, "Auth/Authorize?username=GarievDenis&password=123");
var response = await httpClient.SendAsync(httpRequest);
var _serializerOptions = new JsonSerializerOptions
{
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    WriteIndented = true
};
var token =  await response.Content.ReadFromJsonAsync<Token>(_serializerOptions);
httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.token);

Place place = new Place { Name = "000" };
string json = JsonSerializer.Serialize<Place>(place, _serializerOptions);

int a = 5;
public class Token
{
    public string token { get; set; }
}