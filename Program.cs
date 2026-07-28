using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MachineStatusCheck;

public class Program
{
    public static async Task Main(string[] args)
    {
        /* var configText = File.ReadAllText("Environments.json");
        
        var config = JsonSerializer.Deserialize<AppConfig>(configText); */

        var config = new AppConfig
        {
            Environments =
            [
                new NetBirdEnvironment
                {
                    Name = "Emerald",
                    BaseUrl = "https://netbird.emeraldmep.cloud",
                    Token = "nbp_y4S30rm4h3bcwKOvkokWeaoNMfIgNO2nbtHo"
                },

                new NetBirdEnvironment
                {
                    Name = "Copperline",
                    BaseUrl = "https://netbird.copperline.cloud",
                    Token = "nbp_S8AVVMqtFYQ04FCQ6l3PCmpuMnFsRP1G4RYj"
                }
            ]
        };

        if (config == null || config.Environments.Count < 2)
        {
            Console.WriteLine("Invalid configuration.");
            return;
        }

        var environments = new Dictionary<string, NetBirdEnvironment>
        {
            ["1"] = config.Environments[0],
            ["2"] = config.Environments[1]
        };
        
        Console.WriteLine("NetBird Machine Status Lookup");
        Console.WriteLine(new string('-', 40));

        if (environments.Values.Any(e =>
                string.IsNullOrWhiteSpace(e.BaseUrl) ||
                string.IsNullOrWhiteSpace(e.Token))) 
        {
            Console.WriteLine("Missing one or more required environment variables.");
            Console.WriteLine("Set these before running the program:");
            Console.WriteLine("   https://netbird.emeraldmep.cloud");
            Console.WriteLine("   nbp_y4S30rm4h3bcwKOvkokWeaoNMfIgNO2nbtHo");
            Console.WriteLine("   https://netbird.copperline.cloud");
            Console.WriteLine("   nbp_S8AVVMqtFYQ04FCQ6l3PCmpuMnFsRP1G4RYj");
            PauseBeforeExit();
            return;
        }
        
        Console.Write("Enter NerBird computer name: ");
        var computerName = Console.ReadLine()?.Trim();

        if (string.IsNullOrWhiteSpace(computerName))
        {
            Console.WriteLine("NetBird computer name cannot be empty.");
            PauseBeforeExit();
            return;
        }
        
        Console.WriteLine();
        Console.WriteLine("Emerald or Copperline?");
        Console.WriteLine("   1 = Emerald");
        Console.WriteLine("   2 = Copperline");
        Console.WriteLine("Enter 1 or 2: ");
        
        var apiChoice = Console.ReadLine()?.Trim();

        if (string.IsNullOrWhiteSpace(apiChoice) || !environments.TryGetValue(apiChoice, out var selectedEnvironment))
        {
            Console.WriteLine("Invalid selection.");
            PauseBeforeExit();
            return;
        }

        try
        {
            var peers = await GetPeersAsync(selectedEnvironment);

            if (peers.Count == 0)
            {
                Console.WriteLine($"No peers found in {selectedEnvironment.Name}");
                PauseBeforeExit();
                return;
            }

            var peer = peers.FirstOrDefault(p =>
                string.Equals(p.Name, computerName, StringComparison.OrdinalIgnoreCase) ||
                string.Equals(p.Hostname, computerName, StringComparison.OrdinalIgnoreCase));

            Console.WriteLine();
            Console.WriteLine($"Environment: {selectedEnvironment.Name}");

            if (peer == null)
            {
                Console.WriteLine($"'{computerName}' not found.");
                PauseBeforeExit();
                return;
            }

            var displayName = !string.IsNullOrWhiteSpace(peer.Name)
                ? peer.Name
                : peer.Hostname ?? "(unnamed)";
            Console.Write($"NetBird computer: {displayName} | Status: ");

            Console.ForegroundColor = peer.Connected ? ConsoleColor.Green : ConsoleColor.Red;
            Console.WriteLine(peer.Connected ? "Connected" : "Disconnected");
            Console.ResetColor();
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"HTTP error: {ex.Message}");
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"JSON parsing error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
        }

        PauseBeforeExit();
    }

    private static async Task<List<NetBirdPeer>> GetPeersAsync(NetBirdEnvironment env)
    {
        using var httpClient = new HttpClient
        {
            BaseAddress = new Uri(env.BaseUrl)
        };
        
        httpClient.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
        
        httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Token", env.Token);
        
        var response = await httpClient.GetAsync("/api/peers");
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        
        var peers = JsonSerializer.Deserialize<List<NetBirdPeer>>(
            json,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        
        return peers ?? new List<NetBirdPeer>();
    }

    private static void PauseBeforeExit()
    {
        Console.WriteLine();
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}
