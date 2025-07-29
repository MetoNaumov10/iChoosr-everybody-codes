using Entity;
using System.Globalization;

namespace Services
{
    public class CameraService : ICameraService
    {
       public List<Camera> LoadCameras()
        {
            List<Camera> cameras = new List<Camera>();

            var relativePath = Path.Combine(AppContext.BaseDirectory, @"..\..\..\..\data\cameras-defb.csv");
            var fullPath = Path.GetFullPath(relativePath);
            var lines = File.ReadAllLines(fullPath).Skip(1);

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                var parts = line.Split(';');

                if (parts.Length != 3) continue;

                cameras.Add(new Camera
                { 
                    Name = parts[0].Trim(),
                    Latitude = double.Parse(parts[1], CultureInfo.InvariantCulture),
                    Longitude = double.Parse(parts[2], CultureInfo.InvariantCulture)
                });
            }

            return cameras;
        }
    }
}
