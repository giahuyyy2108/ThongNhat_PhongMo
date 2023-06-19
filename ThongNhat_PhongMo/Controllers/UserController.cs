using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace ThongNhat_PhongMo.Controllers
{
    [Authorize(Roles = "PT-001")]
    public class UserController : Controller
    {
        private readonly HttpClient _httpClient;

        public UserController(HttpClient httpClient) 
        { 
            _httpClient = httpClient;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> getBNbyId(string mabn)
        {
            string apiUrl = $"https://hsoftapi.bvtn.org.vn/api/upd_hsoft_benhnhan/?ip=192.168.0.75&idbv=79025&mabn={mabn}";

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode(); // Throw an exception if the request wasn't successful

                string responseBody = await response.Content.ReadAsStringAsync();
                return Ok(responseBody);
            }
            catch (HttpRequestException ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }
    }
}
