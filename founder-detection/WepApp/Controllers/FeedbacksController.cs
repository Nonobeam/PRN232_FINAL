using Microsoft.AspNetCore.Mvc;
using Model.Models;

namespace WepApp.Controllers
{
    public class FeedbacksController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;

        public FeedbacksController(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiBaseUrl = configuration["ApiSettings:ApiBaseUrl"];
        }

        public async Task<IActionResult> Index()
        {
            var feedbacks = await _httpClient.GetFromJsonAsync<List<Feedback>>(_apiBaseUrl);
            return View(feedbacks);
        }

        public async Task<IActionResult> Details(int id)
        {
            var feedback = await _httpClient.GetFromJsonAsync<Feedback>($"{_apiBaseUrl}/{id}");
            if (feedback == null)
                return NotFound();
            return View(feedback);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Feedback feedback)
        {
            var response = await _httpClient.PostAsJsonAsync(_apiBaseUrl, feedback);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));
            return View(feedback);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var feedback = await _httpClient.GetFromJsonAsync<Feedback>($"{_apiBaseUrl}/{id}");
            if (feedback == null)
                return NotFound();
            return View(feedback);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Feedback feedback)
        {
            var response = await _httpClient.PutAsJsonAsync($"{_apiBaseUrl}/{id}", feedback);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));
            return View(feedback);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var feedback = await _httpClient.GetFromJsonAsync<Feedback>($"{_apiBaseUrl}/{id}");
            if (feedback == null)
                return NotFound();
            return View(feedback);
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
