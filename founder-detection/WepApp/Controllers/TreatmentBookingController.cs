using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using System.Security.Claims;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Authorize]
    public class TreatmentBookingController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;
        private readonly string _servicesApiUrl;
        private readonly string _doctorsApiUrl;

        public TreatmentBookingController(IHttpClientFactory factory, IConfiguration configuration)
        {
            _httpClient = factory.CreateClient("ApiWithAuth");
            var baseUrl = configuration["ApiSettings:BaseUrl"];
            _apiBaseUrl = baseUrl + configuration["ApiSettings:TreatmentBookingPath"];
            _servicesApiUrl = baseUrl + configuration["ApiSettings:ServicesPath"];
            _doctorsApiUrl = baseUrl + configuration["ApiSettings:DoctorsPath"];
        }

        // GET: TreatmentBooking/BookTreatment
        [HttpGet]
        public async Task<IActionResult> BookTreatment()
        {
            // Check if user is Customer
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
            if (userRole != "Customer")
            {
                return Forbid();
            }

            var userId = int.Parse(User.FindFirst("UserId")?.Value ?? "0");
            var userName = User.FindFirst(ClaimTypes.Email)?.Value ?? "Unknown User";

            var model = new BookTreatmentViewModel
            {
                UserId = userId,
                UserName = userName
            };

            try
            {
                // Load available services
                var services = await _httpClient.GetFromJsonAsync<List<Services>>(_servicesApiUrl);
                model.AvailableServices = services?.Where(s => s.IsActive == true).ToList() ?? new List<Services>();

                // Load available doctors
                var doctors = await _httpClient.GetFromJsonAsync<List<DoctorDTO>>(_doctorsApiUrl);
                if (doctors != null)
                {
                    model.AvailableDoctors = doctors.Select(d => new DoctorViewModel
                    {
                        DoctorId = d.DoctorId,
                        FullName = d.FullName,
                        Specialization = d.Specialization,
                        Degree = d.Degree,
                        YearsOfExperience = d.YearsOfExperience,
                        WorkSchedule = d.WorkSchedule,
                        Email = d.Email,
                        PhoneNumber = d.PhoneNumber
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Không thể tải dữ liệu. Vui lòng thử lại sau.";
                // Return empty model if API calls fail
            }

            return View(model);
        }

        // POST: TreatmentBooking/BookTreatment
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BookTreatment(BookTreatmentViewModel model)
        {
            // Check if user is Customer
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
            if (userRole != "Customer")
            {
                return Forbid();
            }

            var userId = int.Parse(User.FindFirst("UserId")?.Value ?? "0");
            model.UserId = userId;
            model.UserName = User.FindFirst(ClaimTypes.Email)?.Value ?? "Unknown User";

            if (!ModelState.IsValid)
            {
                // Reload dropdown data
                await LoadDropdownData(model);
                return View(model);
            }

            try
            {
                // Create booking object
                var booking = new TreatmentBooking
                {
                    UserId = userId,
                    DoctorId = model.DoctorId,
                    ServiceId = model.ServiceId,
                    BookingDate = DateTime.Now,
                    Status = "Chờ xác nhận"
                };

                // Submit booking to API
                var response = await _httpClient.PostAsJsonAsync(_apiBaseUrl, booking);

                if (response.IsSuccessStatusCode)
                {
                    TempData["Success"] = "Đặt lịch điều trị thành công! Chúng tôi sẽ liên hệ với bạn sớm nhất.";
                    return RedirectToAction("MyBookings");
                }
                else
                {
                    TempData["Error"] = "Có lỗi xảy ra khi đặt lịch. Vui lòng thử lại.";
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Có lỗi xảy ra khi đặt lịch. Vui lòng thử lại.";
            }

            // Reload dropdown data and return view
            await LoadDropdownData(model);
            return View(model);
        }

        // GET: TreatmentBooking/MyBookings
        public async Task<IActionResult> MyBookings()
        {
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
            if (userRole != "Customer")
            {
                return Forbid();
            }

            var userId = int.Parse(User.FindFirst("UserId")?.Value ?? "0");
            
            try
            {
                var bookings = await _httpClient.GetFromJsonAsync<List<TreatmentBooking>>($"{_apiBaseUrl}/user/{userId}");
                return View(bookings ?? new List<TreatmentBooking>());
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Không thể tải danh sách đặt lịch. Vui lòng thử lại sau.";
                return View(new List<TreatmentBooking>());
            }
        }

        private async Task LoadDropdownData(BookTreatmentViewModel model)
        {
            try
            {
                // Load available services
                var services = await _httpClient.GetFromJsonAsync<List<Services>>(_servicesApiUrl);
                model.AvailableServices = services?.Where(s => s.IsActive == true).ToList() ?? new List<Services>();

                // Load available doctors
                var doctors = await _httpClient.GetFromJsonAsync<List<DoctorDTO>>(_doctorsApiUrl);
                if (doctors != null)
                {
                    model.AvailableDoctors = doctors.Select(d => new DoctorViewModel
                    {
                        DoctorId = d.DoctorId,
                        FullName = d.FullName,
                        Specialization = d.Specialization,
                        Degree = d.Degree,
                        YearsOfExperience = d.YearsOfExperience,
                        WorkSchedule = d.WorkSchedule,
                        Email = d.Email,
                        PhoneNumber = d.PhoneNumber
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                // Handle silently, empty lists will be used
            }
        }
    }
}
