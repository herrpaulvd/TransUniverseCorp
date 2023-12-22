using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Text;

namespace TransUniverseCorp.Controllers
{
    [Route("/test")]
    public class TestController : Controller
    {
        [Route("")]
        public IActionResult Index()
        {
            StringBuilder result = new();
            using (var client = new System.Net.Http.HttpClient())
            {
                var request = new System.Net.Http.HttpRequestMessage();
                request.RequestUri = new Uri("http://localhost:5291/weatherforecast");
                var response = client.SendAsync(request).Result;
                var json = response.Content.ReadAsStringAsync().Result;
                JArray array = JArray.Parse(json);
                foreach(var item in array)
                {
                    JObject obj = (JObject)item;
                    var summary = obj["summary"];
                    result.Append($"{summary}\n");
                }
            }

            return View("Index", result.ToString());
        }
    }
}
