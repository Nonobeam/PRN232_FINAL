using Microsoft.AspNetCore.Mvc;
using Model.Models;

namespace WebApp.Controllers
{
    public class DoctorsController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;

        public DoctorsController(IHttpClientFactory factory, IConfiguration configuration)
        {
            _httpClient = factory.CreateClient("ApiWithAuth");
            _apiBaseUrl = configuration["ApiSettings:BaseUrl"] + configuration["ApiSettings:DoctorsPath"];
        }

        public async Task<IActionResult> Index()
        {
            var doctors = await _httpClient.GetFromJsonAsync<List<Doctor>>(_apiBaseUrl);
            return View(doctors);
        }

        public async Task<IActionResult> Details(int id)
        {
            var doctor = await _httpClient.GetFromJsonAsync<Doctor>($"{_apiBaseUrl}/{id}");
            if (doctor == null)
                return NotFound();
            return View(doctor);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Doctor doctor)
        {
            var response = await _httpClient.PostAsJsonAsync(_apiBaseUrl, doctor);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));
            return View(doctor);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var doctor = await _httpClient.GetFromJsonAsync<Doctor>($"{_apiBaseUrl}/{id}");
            if (doctor == null)
                return NotFound();
            return View(doctor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Doctor doctor)
        {
            var response = await _httpClient.PutAsJsonAsync($"{_apiBaseUrl}/{id}", doctor);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));
            return View(doctor);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var doctor = await _httpClient.GetFromJsonAsync<Doctor>($"{_apiBaseUrl}/{id}");
            if (doctor == null)
                return NotFound();
            return View(doctor);
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
