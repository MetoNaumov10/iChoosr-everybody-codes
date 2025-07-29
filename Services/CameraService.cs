using Entity;

namespace Services
{
    public class CameraService : ICameraService
    {
       public List<Camera> LoadCameras()
        {
            List<Camera> cameras = new List<Camera>();

            var path = Path.Combine("..", "..", "..", "..", "data", "cameras-defb.csv");
            var lines = File.ReadAllLines(path).Skip(1);

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                var parts = line.Split(';');

                if (parts.Length != 3) continue;

                if (double.TryParse(parts[1], out double lat) && double.TryParse(parts[2], out double lng))
                {
                    cameras.Add(new Camera
                    {
                        Name = parts[0].Trim(),
                        Latitude = lat,
                        Longitude = lng
                    });
                }
            }

            return cameras;
        }
    }
}
