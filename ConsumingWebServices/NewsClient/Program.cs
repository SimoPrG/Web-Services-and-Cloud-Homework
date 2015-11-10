// Write a console application, which searches for news articles by given a query string and a count
// of articles to retrieve. The application should ask the user for input and print the Titles and
// URLs of the articles. For news articles search, use the Feedzilla API and use one of WebClient,
// HttpWebRequest or HttpClient.

namespace NewsClient
{
    using System;
    using System.Linq;
    using System.Net.Http;
    using Newtonsoft.Json.Linq;

    public class Program
    {
        public static void Main()
        {
            Console.Write("Search for (hint: eum): ");
            string query = Console.ReadLine();

            int n = GetPositiveNumberFromConsoleUser("Number of news (there are 5 eums total): ");

            PrintNews(query, n);
            Console.WriteLine("Wait for the news and then press any key to continue...\n");
            Console.ReadKey(true);
        }

        private static int GetPositiveNumberFromConsoleUser(string message)
        {
            int n;
            do
            {
                Console.Write(message);
            }
            while (!(int.TryParse(Console.ReadLine(), out n) && n > 0));

            return n;
        }

        static async void PrintNews(string query, int n)
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://jsonplaceholder.typicode.com/");

            JArray.Parse(await httpClient.GetStringAsync("posts"))
                .Where(news => ((string)news["title"]).Contains(query))
                .Select(news => new
                {
                    Title = news["title"],
                    Body = news["body"]
                })
                .Take(n)
                .ToList()
                .ForEach(news => Console.WriteLine($"Title:\n{news.Title}\nBody:\n{news.Body}\n"));
        }
    }
}