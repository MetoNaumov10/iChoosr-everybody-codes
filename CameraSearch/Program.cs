using Microsoft.Extensions.DependencyInjection;
using Services;

namespace CameraSearch;

public class Program
{

    public static void Main(string[] args)
    {
        var services = new ServiceCollection();
        services.AddScoped<ICameraService, CameraService>();

        var serviceProvider = services.BuildServiceProvider();

        var cameraService = serviceProvider.GetService<ICameraService>();

        // Basic argument parsing
        var nameArgIndex = Array.IndexOf(args, "--name");
        if (nameArgIndex == -1 || nameArgIndex + 1 >= args.Length)
        {
            Console.WriteLine("Usage: dotnet search --name <partial_name>");
            return;
        }

        var searchTerm = args[nameArgIndex + 1];

        // Search for matches (case-insensitive)
        var matches = cameraService?.LoadCameras()
            .Where(n => n.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
            .ToList();

        serviceProvider.Dispose();

        if (matches != null && matches.Count == 0)
        {
            Console.WriteLine($"No matches found for '{searchTerm}'.");
        }
        else
        {
            if (matches == null) return;
            foreach (var camera in matches)
            {
                Console.WriteLine($"- {camera.Number} | {camera.Name} | {camera.Latitude} | {camera.Longitude}");
            }
        }
    }
}