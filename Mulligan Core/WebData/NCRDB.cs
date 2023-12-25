using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mulligan.Core.Models;
using Mulligan.Core.Data;
using Mulligan.Core.WebDTO;
using AngleSharp;
using AngleSharp.Html.Parser;
using AngleSharp.Html;

namespace Mulligan.Core.WebData
{
    public class NCRDB : IDisposable
    {
        private Uri _baseUrl;
        private HttpClientHandler _handler;
        private HttpClient _client;
        private bool disposedValue;
        private string _courseTeeInfoPath = "courseTeeInfo?CourseID=";
        private string _userAgent = "PostmanRuntime/7.35.0";
        private string _baseAddress = "https://ncrdb.usga.org/";

        public NCRDB() 
        {
            _baseUrl = new Uri(_baseAddress);
            _handler = new HttpClientHandler { UseCookies = true };
            _client = new HttpClient(_handler) { BaseAddress = _baseUrl };
            _client.DefaultRequestHeaders.UserAgent.ParseAdd(_userAgent);
            _client.DefaultRequestHeaders.Accept.ParseAdd("*/*");
        }

        public async Task<List<NCRDBTee>> GetTees(int courseId, int facilityId)
        {
            List<NCRDBTee> tees = [];

            var pageContent = await GetTeesPage(courseId);
            tees = await ParseTeePage(pageContent);

            return tees;
        }

        private async Task<string> GetTeesPage(int courseId)
        {
            var query = $"{_courseTeeInfoPath}{courseId}"; ;
            var response = await _client.GetAsync(query);
            var pageContent = await response.Content.ReadAsStringAsync();
            return pageContent;
        }

        private async Task<List<NCRDBTee>> ParseTeePage(string html)
        {
            List<NCRDBTee> tees = [];
            IConfiguration config = Configuration.Default;
            IBrowsingContext context = BrowsingContext.New(config);
            var parser = new HtmlParser();
            var doc = parser.ParseDocument(html);
            

            var table = doc.QuerySelector("#gvTee");
            var rows = table.QuerySelectorAll("tr");
            int rowIndex = 0;
            int rowCount = rows.Count();
            for (int curRow = 1; curRow <  rowCount; curRow++)
            {
                var cells = rows[curRow].QuerySelectorAll("td");
                var frontData = cells[8].TextContent.Split('/', StringSplitOptions.TrimEntries);
                var backData = cells[9].TextContent.Split('/', StringSplitOptions.TrimEntries);
                /*
                 * 0 tee name
                 * 1 gender
                 * 2 par
                 * 3 course rating
                 * 4 bogey rating
                 * 5 slope
                 * 6 front rating
                 * 7 back rating
                 * 8 front rating / slope
                 * 9 back rating / slope
                 * */

                NCRDBTee tee = new NCRDBTee(
                        cells[0].TextContent,
                        cells[1].TextContent,
                        int.Parse(cells[2].TextContent),
                        decimal.Parse(cells[3].TextContent),
                        decimal.Parse(cells[4].TextContent),
                        int.Parse(cells[5].TextContent),
                        decimal.Parse(cells[6].TextContent),
                        int.Parse(frontData[1]),
                        decimal.Parse(cells[7].TextContent),
                        int.Parse(backData[1])
                        );
                tees.Add(tee);
                    
            }
            return tees;

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
        // ~NCRDB()
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
