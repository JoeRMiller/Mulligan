using AngleSharp.Html.Parser;
using AngleSharp;
using Mulligan.Core.WebDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using AngleSharp;
using AngleSharp.Html.Parser;
using AngleSharp.Html;

namespace Mulligan.Core.WebData
{
    public class BlueGolf : IDisposable
    {
        
        

        private Uri _baseUrl;
        private HttpClientHandler _handler;
        private HttpClient _client;
        private string _courseTeeInfoPath = "courseTeeInfo?CourseID=";
        private string _userAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:121.0) Gecko/20100101 Firefox/121.0";
        //private string _baseAddress = "https://course.bluegolf.com/bluegolf/course/course/directory.htm";
        private string _baseAddress = "https://course.bluegolf.com/bluegolf/course/course/";

        private bool disposedValue;

        public BlueGolf() 
        {
            _baseUrl = new Uri(_baseAddress);
            _handler = new HttpClientHandler { UseCookies = true };
            _client = new HttpClient(_handler) { BaseAddress = _baseUrl };
            _client.DefaultRequestHeaders.UserAgent.ParseAdd(_userAgent);
            _client.DefaultRequestHeaders.Accept.ParseAdd("*/*");
        }

        private async Task<string> GetSearchPage(string courseName)
        {
            Console.WriteLine(courseName);
            var escaped = Uri.EscapeDataString(courseName);
            var builder = new UriBuilder(_client.BaseAddress + "directory.htm");
            builder.Query = $"q={escaped}&public=Y&private=Y";
            var response = await _client.GetAsync(builder.Uri);
            var pageContent = await response.Content.ReadAsStringAsync();
            return pageContent;
        }

        private async Task GetCourseURI(string pageHtml)
        {
            IConfiguration config = Configuration.Default;
            IBrowsingContext context = BrowsingContext.New(config);
            var parser = new HtmlParser();
            var doc = parser.ParseDocument(pageHtml);
            var table = doc.QuerySelector("table.table.border.table-striped");
            //Console.WriteLine(table.TagName);
            //Console.WriteLine(table.Children.Count());
            //Console.WriteLine(table.Children[0].TagName);
            //Console.WriteLine(table.Children[1].TagName);

            var tableBody = table.QuerySelector("tbody");

            var selector = "td.pl-4:has(a):has(input)";
            var rows = tableBody.QuerySelectorAll(selector);

            //var rows = tableBody.QuerySelector("#p1-4");
            string champ = "LEGEND";
            foreach (var row in rows)
            {
                // Process each row as needed
                // Example: Output the HTML of each row
                var aTag = row.QuerySelector("a");
                var aText = aTag?.TextContent;
                var inputTag = row.QuerySelector("input");
                var inputValue = inputTag?.GetAttribute("value"); // '?.GetAttribute' handles if inputTag is null

                if (aText.ToLower().Contains(champ.ToLower()))
                {
                    Console.WriteLine($"{_baseAddress}{inputValue}/detailedscorecard.htm");
                }

                //Console.WriteLine("a Tag Text: " + aText);
                //Console.WriteLine("Input Tag Value: " + inputValue);
            }
            //for (int i = 1; i < rows.Children.Count(); i++)
            //{
            //Console.WriteLine(rows[i].InnerText);
            //}
            //Console.WriteLine(rows.Children.Count());

        }

        public async Task<List<HoleDTO>> FetchScoreCard(string courseName)
        {
            List<HoleDTO> holes = [];
            var page = await GetSearchPage(courseName);
            await GetCourseURI(page);


            return holes;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                    _handler?.Dispose();
                    _client?.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~BlueGolf()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}

