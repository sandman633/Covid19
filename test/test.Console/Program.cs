using System;


class Program
{
    private const string data_url = @"https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_confirmed_global.csv";
    static async Task Main(string[] args)
    {


        var client = new HttpClient();

        var response = await client.GetAsync(data_url);
        var content = response.Content.ReadAsStringAsync().Result;

        Console.WriteLine();

        Console.ReadLine();
    }
}
