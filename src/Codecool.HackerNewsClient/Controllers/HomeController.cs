using Codecool.HackerNewsClient.Models;
using HackerNewsClient.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace HackerNewsClient.Controllers
{
    /// <summary>
    /// HomeController is a generic controller responsible for communicating with
    /// external or internal data sources (API or other data services).
    /// It contains methods communicating with the external API and
    /// serializing the data into News object.
    /// The methods return ActionResult which generates respective HTML page (View)
    /// </summary>
    public class HomeController : Controller
    {
        const string apiBaseURL = "https://api.hnpwa.com/v0/";

        private readonly HttpClient _httpClient;
        private List<News> NewsItems { get; set; }

        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _httpClient = new HttpClient();
        }

        /// <summary>
        /// returns index page with top news
        /// </summary>
        /// <param name="page"> parameter of current page index </param>
        /// <returns></returns>
        public async Task<ActionResult> Index(int page = 1)
        {
            try
            {
                ViewData["currentPage"] = page;
                string myURL = $"{apiBaseURL}/news/{page}.json";
                var response = await _httpClient.GetAsync(myURL);
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                NewsItems = JsonConvert.DeserializeObject<List<News>>(responseBody);
                return View(NewsItems);
            }
            catch
            {
                return View("Error", new ErrorViewModel());
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
