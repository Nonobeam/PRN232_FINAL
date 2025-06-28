using Microsoft.AspNetCore.Mvc;
using Model.Models;

namespace WebApp.Controllers
{
    public class UsersController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;

        public UsersController(IHttpClientFactory factory, IConfiguration configuration)
        {
            _httpClient = factory.CreateClient("ApiWithAuth");
            _apiBaseUrl = configuration["ApiSettings:BaseUrl"] + configuration["ApiSettings:UsersPath"];
        }

        public async Task<IActionResult> Index()
        {
            var users = await _httpClient.GetFromJsonAsync<List<User>>(_apiBaseUrl);
            return View(users);
        }

        public async Task<IActionResult> Details(int id)
        {
            var user = await _httpClient.GetFromJsonAsync<User>($"{_apiBaseUrl}/{id}");
            if (user == null)
                return NotFound();
            return View(user);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User user)
        {
            var response = await _httpClient.PostAsJsonAsync(_apiBaseUrl, user);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));
            return View(user);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var user = await _httpClient.GetFromJsonAsync<User>($"{_apiBaseUrl}/{id}");
            if (user == null)
                return NotFound();
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, User user)
        {
            var response = await _httpClient.PutAsJsonAsync($"{_apiBaseUrl}/{id}", user);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));
            return View(user);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var user = await _httpClient.GetFromJsonAsync<User>($"{_apiBaseUrl}/{id}");
            if (user == null)
                return NotFound();
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_apiBaseUrl}/{id}");
            return RedirectToAction(nameof(Index));
        }
    }
}
