using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Security.Authentication;

namespace Mulligan.Command
{
    internal class SearchCourse
    {

        public async Task GetPage(string address)
        {
            var baseAddress = new Uri("https://ncrdb.usga.org/");
            //var baseAddress = new Uri("https://adventofcode.com/");

            try
            {
                using (var handler = new HttpClientHandler { UseCookies = true })
                {
                    //handler.AllowAutoRedirect = false;
                    //handler.ClientCertificateOptions = ClientCertificateOption.Manual;
                    //handler.SslProtocols = SslProtocols.Tls12;
                    //handler.ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) =>
                    //{
                    //    return true; // Bypass SSL certificate checks (use for debugging only)
                    //};
                    using (var client = new HttpClient(handler) { BaseAddress = baseAddress })
                    {
                        client.Timeout = TimeSpan.FromSeconds(5);

                        string firefoxUserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:121.0) Gecko/20100101 Firefox/121.0";
                        client.DefaultRequestHeaders.UserAgent.ParseAdd(firefoxUserAgent);
                        client.DefaultRequestHeaders.Accept.ParseAdd("*/*");
                        //client.DefaultRequestHeaders.AcceptLanguage.ParseAdd("en-US,en;q=0.5");
                        // Note: HttpClient handles Accept-Encoding automatically


                        // Send a GET request to fetch the initial page and CSRF token
                        foreach (var header in client.DefaultRequestHeaders)
                        {
                            Console.WriteLine($"{header.Key}: {string.Join(", ", header.Value)}");
                        }

                        
                        Console.WriteLine("Initial");
                        var initialResponse = await client.GetAsync("/").ConfigureAwait(false);

                        //var initialResponse = await client.GetAsync(baseAddress);
                        Console.WriteLine("Page");
                        var pageContent = await initialResponse.Content.ReadAsStringAsync();
                        Console.WriteLine("CSRF");
                        var csrfToken = ExtractCsrfToken(pageContent); // Implement this based on the HTML structure

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
        public async Task GetFormDataAsync()
        {
            var baseAddress = new Uri("https://ncrdb.usga.org/");

            using (var handler = new HttpClientHandler { UseCookies = true })
            using (var client = new HttpClient(handler) { BaseAddress = baseAddress })
            {
                // Send a GET request to fetch the initial page and CSRF token
                var initialResponse = await client.GetAsync(@"/");
                var pageContent = await initialResponse.Content.ReadAsStringAsync();
                var csrfToken = ExtractCsrfToken(pageContent); // Implement this based on the HTML structure

                // Build the POST request with form data
                var content = new FormUrlEncodedContent(new[]
                {
                    //new KeyValuePair<string, string>("clubName", "Sample Club Name"),
                    new KeyValuePair<string, string>("clubCity", "Oceanside"),
                    //new KeyValuePair<string, string>("clubState", "US-CA"),
                    new KeyValuePair<string, string>("clubCountry", "USA"),
                    // Other form fields as needed
                });

                // Add CSRF token to request headers
                client.DefaultRequestHeaders.Add("RequestVerificationToken", csrfToken);

                // Send the POST request
                var result = await client.PostAsync("/NCRListing?handler=LoadCourses", content);

                // Read and process the response
                var resultContent = await result.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<dynamic>(resultContent);

                // Now you can work with responseData
            }
        }

        private string ExtractCsrfToken(string html)
        {
            // Logic to extract CSRF token from the HTML content
            return "Extracted_Token_Value";
        }

        //var uri = "https://ncrdb.usga.org/NCRListing?handler=LoadCourses";

        /*

        public async Task PostFormDataAsync()
        {
            using (var client = new HttpClient())
            {
                // Replace with the actual URL where the form is posted
                var uri = "https://ncrdb.usga.org/NCRListing?handler=LoadCourses";

                // Create the content to send
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("CountryName", "USA"),
                    new KeyValuePair<string, string>("clubName", ""),
                    new KeyValuePair<string, string>("clubCity", "Oceanside"),
                    new KeyValuePair<string, string>("StateName", "US-CA"),
                    // Include the anti-forgery token if it's required
                    //new KeyValuePair<string, string>("__RequestVerificationToken", "Your_Token_Value_Here")
                });
                Console.WriteLine("Posting");
                // Post the data
                var result = await client.PostAsync(uri, content);
                Console.WriteLine(result);
                // Read the result (optional)
                var resultContent = await result.Content.ReadAsStringAsync();
                Console.WriteLine(resultContent);
            }
        }
        */
    }
}
