using Mulligan.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Mulligan.CLI
{
    public record FacilityRecord
    {
        public int courseID;
        public string courseName;
        public int facilityID;
        public string facilityName;
        public string fullName;
        public string address1;
        public string address2;
        public string city;
        public string state;
        public string country;
        public int entCountryCode;
        public int entStateCode;
        public string? phoneNumber;
        public string? email;
    }
    public class Facilitator
    {
        private Uri _baseAddress = new Uri("https://ncrdb.usga.org/");
        private string _userAgent = "PostmanRuntime/7.35.0";
        private string _coursesUrl = "/NCRListing?handler=LoadCourses";
        private string _facilities;

        public Facilitator()
        {

        }
        
        public string Facilities { get => _facilities; set => _facilities = value; }

        public async Task LoadStateCourses(string state)
        {
            var clubState = $"US-{state}";
            var handler = new HttpClientHandler();
            handler.CookieContainer = new CookieContainer();
            var client = new HttpClient(handler);
            client.BaseAddress = _baseAddress;
            
            client.DefaultRequestHeaders.UserAgent.ParseAdd(_userAgent);
            client.DefaultRequestHeaders.Accept.ParseAdd("*/*");

            var response = await client.GetAsync(_baseAddress);
            var pageContent = await response.Content.ReadAsStringAsync();
            string csrf = ExtractCsrfToken(pageContent);

            var content = new FormUrlEncodedContent(new[]
            {
                //new KeyValuePair<string, string>("clubName", "Oaks North"),
                //new KeyValuePair<string, string>("clubCity", "Oceanside"),
                new KeyValuePair<string, string>("clubState", clubState),
                new KeyValuePair<string, string>("clubCountry", "USA"),
            });

            client.DefaultRequestHeaders.Add("RequestVerificationToken", csrf);
            client.DefaultRequestHeaders.Add("Origin", _baseAddress.AbsoluteUri);
            client.DefaultRequestHeaders.Referrer = _baseAddress;

            var result = await client.PostAsync(_coursesUrl, content);

            _facilities = await result.Content.ReadAsStringAsync();
        }

        private string ExtractCsrfToken(string html)
        {
            var regex = new Regex(@"<input[^>]*name=['""]__RequestVerificationToken['""][^>]*value=['""]([^'""]+)['""][^>]*>", RegexOptions.IgnoreCase);
            var match = regex.Match(html);

            if (match.Success)
            {
                return match.Groups[1].Value;
            }

            return String.Empty;
        }
    }
}
