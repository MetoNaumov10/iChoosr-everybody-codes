using Entity;
using System.Globalization;
using System.Text.RegularExpressions;

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

                Match match = Regex.Match(parts[0], @"\d+");
                string cameraNumber = "";

                if (match.Success)
                {
                    cameraNumber = match.Value;
                }

                cameras.Add(new Camera
                { 
                    Number = int.Parse(cameraNumber),
                    Name = parts[0].Trim(),
                    Latitude = double.Parse(parts[1], CultureInfo.InvariantCulture),
                    Longitude = double.Parse(parts[2], CultureInfo.InvariantCulture)
                });
            }

            return cameras;
        }
    }
}
