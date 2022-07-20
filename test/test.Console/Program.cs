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
            yield return line.Replace("Korea,","Korea - ").Replace("Bonaire,", "Bonaire - ");
        }
    }

    private static IEnumerable<(string CountryName,string Province, int[] Counts)> GetInjuredPeopelsDataByCountry()
    {
        var lines = GetDataLines()
            .Skip(1)
            .Select(e=>e.Split(','));

        foreach(var line in lines)
        {
            var province = line[0].Trim();
            var country = line[1].Trim(' ','"');
            var counts = line.Skip(4)
                .Select(int.Parse)
                .ToArray();
            yield return (country, province, counts);
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

        var russia = GetInjuredPeopelsDataByCountry().First(e => e.CountryName.Equals("Russia", StringComparison.OrdinalIgnoreCase));


        Console.WriteLine(String.Join("\r\n",dates.Zip(russia.Counts,(date,count)=>$"{date} - {count}")));



        //foreach (var line in GetDataLines())
        //    Console.WriteLine(line);

        Console.ReadLine();
    }
}
