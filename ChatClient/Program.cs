using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

var connection = new HubConnectionBuilder()
        .WithUrl("https://localhost:7160/chathub")
        .Build();

connection.On<string, string>("ReceiveMessage", (user, message) =>
{
    Console.WriteLine($"{user}: {message}");
});

await connection.StartAsync();

Console.WriteLine("Connected. Enter your name:");
var userName = Console.ReadLine();

while (true)
{
    var message = Console.ReadLine();

    if (string.IsNullOrEmpty(message))
    {
        break;
    }

    await connection.SendAsync("SendMessage", userName, message);
}

await connection.DisposeAsync();
