using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Pr4
{
    public static class WeatherService
    {
        public static List<WeatherData> LoadWeatherData(string filePath)
        {
            var list = new List<WeatherData>();
            var lines = File.ReadAllLines(filePath);

            for (int i = 1; i < lines.Length; i++) // pomijamy nagłówek
            {
                var parts = lines[i].Split(',');

                if (parts.Length >= 4)
                {
                    list.Add(new WeatherData
                    {
                        Date = parts[0],
                        Temperature = double.Parse(parts[1], CultureInfo.InvariantCulture),
                        Humidity = double.Parse(parts[2], CultureInfo.InvariantCulture),
                        Pressure = double.Parse(parts[3], CultureInfo.InvariantCulture)
                    });
                }
            }

            return list;
        }
    }
}
