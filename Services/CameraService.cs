using Entity;

namespace Services
{
    public class CameraService : ICameraService
    {
       public List<Camera> LoadCameras()
        {
            List<Camera> cameras = new List<Camera>();

            var lines = File.ReadAllLines(@"..\data\cameras-defb.csv").Skip(1);

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
