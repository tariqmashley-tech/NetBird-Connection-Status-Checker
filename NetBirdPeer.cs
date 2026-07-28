using System.Text.Json.Serialization;

namespace MachineStatusCheck;

public class NetBirdEnvironment
{
    public string Name { get; set; } = "";
    public string BaseUrl { get; set; } = "";
    public string Token { get; set; } = "";
}
