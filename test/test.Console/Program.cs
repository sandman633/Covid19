using System;
using System.Globalization;

class Program
{
    private const string data_url = @"https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_confirmed_global.csv";

    public static IFormatProvider? CulterInfo { get; private set; }

    private static async Task<Stream> GetDataStream()
    {
        var client = new HttpClient();
        var response = await client.GetAsync(data_url, HttpCompletionOption.ResponseHeadersRead);
        return await response.Content.ReadAsStreamAsync();
    }

    private static async Task<string> GetDataString()
    {
        var client = new HttpClient();
        var response = await client.GetAsync(data_url, HttpCompletionOption.ResponseHeadersRead);
        return await response.Content.ReadAsStringAsync();
    }
    private static IEnumerable<string> GetDataLines()
    {
        using var dataStream = GetDataStream().Result;
        using var dataReader = new StreamReader(dataStream);
        while (!dataReader.EndOfStream)
        {
            var line = dataReader.ReadLine();
            if (string.IsNullOrWhiteSpace(line)) continue;
            yield return line;
        }
    }

    private static DateTime[] GetDateTimes() => GetDataLines()
        .First()
        .Split(',')
        .Skip(4)
        .Select(e => DateTime.Parse(e, CultureInfo.InvariantCulture)).ToArray();

    static async Task Main(string[] args)
    {


        var dates = GetDateTimes();

        foreach(var date in dates)
            Console.WriteLine(date);
        


        //foreach (var line in GetDataLines())
        //    Console.WriteLine(line);

        Console.ReadLine();
    }
}
