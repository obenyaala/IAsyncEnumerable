// See https://aka.ms/new-console-template for more information
using DTOs;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text.Json;

Console.WriteLine("Hello, World!");

var httpClient = new HttpClient
{
    BaseAddress = new Uri("https://localhost:7073/WeatherForecast/")
};

var msg = new HttpRequestMessage(HttpMethod.Get, new Uri(httpClient.BaseAddress, "IAsyncTest"));

var response = await httpClient.SendAsync(msg, HttpCompletionOption.ResponseHeadersRead);

response.EnsureSuccessStatusCode();

using var stream = await response.Content.ReadAsStreamAsync();
var streamReader = new StreamReader(stream);

var reader = new JsonTextReader(streamReader);
var setting = new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All };
var seri = Newtonsoft.Json.JsonSerializer.Create(setting);

while (await reader.ReadAsync())
{
    if (reader.TokenType == JsonToken.StartObject)
    {
        var item = seri.Deserialize<TestDTO>(reader);
        Console.WriteLine($"int: {item.IntValue}, string: {item.StringValue}");
    }
}

//var result = await response.Content.ReadFromJsonAsync<IAsyncEnumerable<TestDTO>>();

//if (result == null) return;

//await foreach (var item in result)
//{
//    Console.WriteLine($"int: {item.IntValue}, string: {item.StringValue}");
//}

Console.ReadLine();