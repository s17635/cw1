using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace cw1
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            // var newPerson = new Person { FirstName = "Ola" };
            // HTTP GET, HTTP POST, HTTP PUT, HTTP PATCH, HTTP DELETE
            
            

            if (args.Length >= 1)
            {                
                var url = args[0];
                if (Uri.IsWellFormedUriString(url, UriKind.Absolute))
                {
                    using (var httpClient = new HttpClient()) // żeby Dispose się wykonał
                    {
                        using (var response = await httpClient.GetAsync(url))

                            if (response.IsSuccessStatusCode)
                            {
                                string htmlContent = await response.Content.ReadAsStringAsync();
                                var regex = new Regex("[a-z]+[a-z0-9]*@[a-z0-9]+(\\.[a-z0-9]+)?\\.[a-z]+", RegexOptions.IgnoreCase);
                                var matches = regex.Matches(htmlContent);

                                if (matches.Count == 0)
                                {
                                    Console.WriteLine("Nie znaleziono adresów email");
                                }
                                else
                                {
                                    List<string> usedEmails = new List<string>();
                                    foreach (var match in matches)
                                    {
                                        string matchString = match.ToString();
                                        if (!usedEmails.Contains(matchString))
                                        {
                                            Console.WriteLine(matchString);
                                            usedEmails.Add(matchString);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("Błąd w czasie pobierania strony");
                            }
                    }
                }
                else
                {
                    throw new ArgumentException();
                }
            }
            else
            {
                throw new ArgumentNullException();
            }

        }
    }
}
