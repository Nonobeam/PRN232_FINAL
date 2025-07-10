using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly HttpClient _httpClient;
    private readonly string _apiBaseUrl;

    public HomeController(ILogger<HomeController> logger, IHttpClientFactory factory, IConfiguration configuration)
    {
        _logger = logger;
        _httpClient = factory.CreateClient("ApiWithAuth");
        _apiBaseUrl = configuration["ApiSettings:BaseUrl"] ?? "https://localhost:7001";
    }

    public async Task<IActionResult> Index()
    {
        var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
        var userId = User.FindFirst("UserId")?.Value;

        var dashboardModel = new DashboardViewModel
        {
            UserRole = userRole ?? "Guest",
            UserId = int.Parse(userId ?? "0"),
            UserName = User.FindFirst(ClaimTypes.Email)?.Value ?? "Unknown User"
        };

        // Load role-specific data
        switch (userRole)
        {
            case "Admin":
                await LoadAdminDashboardData(dashboardModel);
                break;
            case "Manager":
                await LoadManagerDashboardData(dashboardModel);
                break;
            case "Doctor":
                await LoadDoctorDashboardData(dashboardModel);
                break;
            case "Customer":
                await LoadCustomerDashboardData(dashboardModel);
                break;
        }

        return View(dashboardModel);
    }

    private async Task LoadAdminDashboardData(DashboardViewModel model)
    {
        try
        {
            // Load system-wide statistics
            var usersResponse = await _httpClient.GetAsync($"{_apiBaseUrl}/api/User");
            if (usersResponse.IsSuccessStatusCode)
            {
                var users = await usersResponse.Content.ReadFromJsonAsync<List<dynamic>>();
                model.TotalUsers = users?.Count ?? 0;
            }

            var doctorsResponse = await _httpClient.GetAsync($"{_apiBaseUrl}/api/Doctor");
            if (doctorsResponse.IsSuccessStatusCode)
            {
                var doctors = await doctorsResponse.Content.ReadFromJsonAsync<List<dynamic>>();
                model.TotalDoctors = doctors?.Count ?? 0;
            }

            var bookingsResponse = await _httpClient.GetAsync($"{_apiBaseUrl}/api/TreatmentBooking");
            if (bookingsResponse.IsSuccessStatusCode)
            {
                var bookings = await bookingsResponse.Content.ReadFromJsonAsync<List<dynamic>>();
                model.TotalBookings = bookings?.Count ?? 0;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading admin dashboard data");
        }
    }

    private async Task LoadManagerDashboardData(DashboardViewModel model)
    {
        try
        {
            // Load management statistics
            var bookingsResponse = await _httpClient.GetAsync($"{_apiBaseUrl}/api/TreatmentBooking");
            if (bookingsResponse.IsSuccessStatusCode)
            {
                var bookings = await bookingsResponse.Content.ReadFromJsonAsync<List<dynamic>>();
                model.TotalBookings = bookings?.Count ?? 0;
            }

            var servicesResponse = await _httpClient.GetAsync($"{_apiBaseUrl}/api/Services");
            if (servicesResponse.IsSuccessStatusCode)
            {
                var services = await servicesResponse.Content.ReadFromJsonAsync<List<dynamic>>();
                model.TotalServices = services?.Count ?? 0;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading manager dashboard data");
        }
    }

    private async Task LoadDoctorDashboardData(DashboardViewModel model)
    {
        try
        {
            // Get doctor ID first
            var doctorResponse = await _httpClient.GetAsync($"{_apiBaseUrl}/api/Doctor/user/{model.UserId}");
            if (doctorResponse.IsSuccessStatusCode)
            {
                var doctor = await doctorResponse.Content.ReadFromJsonAsync<dynamic>();
                var doctorId = doctor?.GetProperty("doctorId").GetInt32();

                if (doctorId.HasValue)
                {
                    // Load doctor's appointments
                    var appointmentsResponse = await _httpClient.GetAsync($"{_apiBaseUrl}/api/TreatmentBooking/doctor/{doctorId}");
                    if (appointmentsResponse.IsSuccessStatusCode)
                    {
                        var appointments = await appointmentsResponse.Content.ReadFromJsonAsync<List<dynamic>>();
                        model.MyAppointments = appointments?.Count ?? 0;
                    }

                    // Load doctor's medical records
                    var recordsResponse = await _httpClient.GetAsync($"{_apiBaseUrl}/api/MedicalRecord/doctor/{doctorId}");
                    if (recordsResponse.IsSuccessStatusCode)
                    {
                        var records = await recordsResponse.Content.ReadFromJsonAsync<List<dynamic>>();
                        model.MyMedicalRecords = records?.Count ?? 0;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading doctor dashboard data");
        }
    }

    private async Task LoadCustomerDashboardData(DashboardViewModel model)
    {
        try
        {
            // Load customer's bookings
            var bookingsResponse = await _httpClient.GetAsync($"{_apiBaseUrl}/api/TreatmentBooking/user/{model.UserId}");
            if (bookingsResponse.IsSuccessStatusCode)
            {
                var bookings = await bookingsResponse.Content.ReadFromJsonAsync<List<dynamic>>();
                model.MyBookings = bookings?.Count ?? 0;
            }

            // Load customer's upcoming schedules
            if (model.MyBookings > 0)
            {
                var schedulesResponse = await _httpClient.GetAsync($"{_apiBaseUrl}/api/TreatmentSchedule");
                if (schedulesResponse.IsSuccessStatusCode)
                {
                    var schedules = await schedulesResponse.Content.ReadFromJsonAsync<List<dynamic>>();
                    model.UpcomingSchedules = schedules?.Count ?? 0;
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading customer dashboard data");
        }
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
