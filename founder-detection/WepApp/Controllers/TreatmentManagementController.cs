using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApp.Models;
using Model.Models;

namespace WebApp.Controllers
{
    public class TreatmentManagementController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;
        private readonly string _doctorsApiUrl;
        private readonly string _servicesApiUrl;

        public TreatmentManagementController(IHttpClientFactory factory, IConfiguration configuration)
        {
            _httpClient = factory.CreateClient("ApiWithAuth");
            _apiBaseUrl = configuration["ApiSettings:BaseUrl"] + configuration["ApiSettings:TreatmentBookingPath"];
            _doctorsApiUrl = configuration["ApiSettings:BaseUrl"] + configuration["ApiSettings:DoctorsPath"];
            _servicesApiUrl = configuration["ApiSettings:BaseUrl"] + configuration["ApiSettings:ServicesPath"];
        }

        // Test action to verify controller is working
        public IActionResult Test()
        {
            return Json(new { message = "TreatmentManagement controller is working!", apiUrl = _apiBaseUrl });
        }

        // GET: TreatmentManagement
        public async Task<IActionResult> Index(string filter = "pending")
        {
            // Check if user is Manager or Admin
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
            if (userRole != "Manager" && userRole != "Admin")
            {
                return Forbid();
            }

            var model = new TreatmentManagementViewModel
            {
                CurrentFilter = filter
            };

            try
            {
                // Get all bookings
                var bookings = await _httpClient.GetFromJsonAsync<List<TreatmentBooking>>(_apiBaseUrl);
                
                if (bookings != null)
                {
                    // Convert to ViewModels
                    var bookingViewModels = await ConvertToViewModels(bookings);
                    
                    // Filter bookings by status
                    model.PendingBookings = bookingViewModels.Where(b => b.Status == "Chờ xác nhận").ToList();
                    model.AcceptedBookings = bookingViewModels.Where(b => b.Status == "Đã xác nhận" || b.Status == "Đã đặt lịch").ToList();
                    model.RejectedBookings = bookingViewModels.Where(b => b.Status == "Đã từ chối").ToList();
                    
                    // Set counts
                    model.TotalPending = model.PendingBookings.Count;
                    model.TotalAccepted = model.AcceptedBookings.Count;
                    model.TotalRejected = model.RejectedBookings.Count;
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Không thể tải dữ liệu đặt lịch. Vui lòng thử lại sau.";
            }

            return View(model);
        }

        // GET: TreatmentManagement/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
            if (userRole != "Manager" && userRole != "Admin")
            {
                return Forbid();
            }

            try
            {
                var booking = await _httpClient.GetFromJsonAsync<TreatmentBooking>($"{_apiBaseUrl}/{id}");
                if (booking == null)
                {
                    return NotFound();
                }

                var bookingViewModel = await ConvertToViewModel(booking);
                return View(bookingViewModel);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Không thể tải thông tin đặt lịch.";
                return RedirectToAction("Index");
            }
        }

        // GET: TreatmentManagement/Accept/5
        public async Task<IActionResult> Accept(int id)
        {
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
            if (userRole != "Manager" && userRole != "Admin")
            {
                return Forbid();
            }

            try
            {
                var booking = await _httpClient.GetFromJsonAsync<TreatmentBooking>($"{_apiBaseUrl}/{id}");
                if (booking == null)
                {
                    return NotFound();
                }

                if (booking.Status != "Chờ xác nhận")
                {
                    TempData["Error"] = "Chỉ có thể xác nhận các đặt lịch đang chờ xác nhận.";
                    return RedirectToAction("Index");
                }

                var model = new AcceptTreatmentViewModel
                {
                    BookingId = booking.BookingId,
                    UserName = booking.User?.FullName ?? "N/A",
                    ServiceName = booking.Service?.Name ?? "N/A",
                    DoctorName = booking.Doctor?.User?.FullName ?? "N/A",
                    BookingDate = booking.BookingDate
                };

                return View(model);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Không thể tải thông tin đặt lịch.";
                return RedirectToAction("Index");
            }
        }

        // POST: TreatmentManagement/Accept
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Accept(AcceptTreatmentViewModel model)
        {
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
            if (userRole != "Manager" && userRole != "Admin")
            {
                return Forbid();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                // Update booking status using the new endpoint
                var statusUpdate = new { Status = "Đã xác nhận" };
                var response = await _httpClient.PutAsJsonAsync($"{_apiBaseUrl}/{model.BookingId}/status", statusUpdate);
                
                if (response.IsSuccessStatusCode)
                {
                    TempData["Success"] = $"Đã xác nhận đặt lịch điều trị cho {model.UserName}.";
                    return RedirectToAction("Index");
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    TempData["Error"] = $"Có lỗi xảy ra khi xác nhận đặt lịch. Status: {response.StatusCode}, Error: {errorContent}";
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Có lỗi xảy ra khi xác nhận đặt lịch: {ex.Message}";
            }

            return View(model);
        }

        // GET: TreatmentManagement/Reject/5
        public async Task<IActionResult> Reject(int id)
        {
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
            if (userRole != "Manager" && userRole != "Admin")
            {
                return Forbid();
            }

            try
            {
                var booking = await _httpClient.GetFromJsonAsync<TreatmentBooking>($"{_apiBaseUrl}/{id}");
                if (booking == null)
                {
                    return NotFound();
                }

                if (booking.Status != "Chờ xác nhận")
                {
                    TempData["Error"] = "Chỉ có thể từ chối các đặt lịch đang chờ xác nhận.";
                    return RedirectToAction("Index");
                }

                var model = new RejectTreatmentViewModel
                {
                    BookingId = booking.BookingId,
                    UserName = booking.User?.FullName ?? "N/A",
                    ServiceName = booking.Service?.Name ?? "N/A",
                    DoctorName = booking.Doctor?.User?.FullName ?? "N/A",
                    BookingDate = booking.BookingDate
                };

                return View(model);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Không thể tải thông tin đặt lịch.";
                return RedirectToAction("Index");
            }
        }

        // POST: TreatmentManagement/Reject
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reject(RejectTreatmentViewModel model)
        {
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
            if (userRole != "Manager" && userRole != "Admin")
            {
                return Forbid();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                // Update booking status using the new endpoint
                var statusUpdate = new { Status = "Đã từ chối" };
                var response = await _httpClient.PutAsJsonAsync($"{_apiBaseUrl}/{model.BookingId}/status", statusUpdate);
                
                if (response.IsSuccessStatusCode)
                {
                    TempData["Success"] = $"Đã từ chối đặt lịch điều trị cho {model.UserName}.";
                    return RedirectToAction("Index");
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    TempData["Error"] = $"Có lỗi xảy ra khi từ chối đặt lịch. Status: {response.StatusCode}, Error: {errorContent}";
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Có lỗi xảy ra khi từ chối đặt lịch: {ex.Message}";
            }

            return View(model);
        }

        private async Task<List<TreatmentBookingViewModel>> ConvertToViewModels(List<TreatmentBooking> bookings)
        {
            var result = new List<TreatmentBookingViewModel>();
            
            foreach (var booking in bookings)
            {
                result.Add(await ConvertToViewModel(booking));
            }
            
            return result;
        }

        private async Task<TreatmentBookingViewModel> ConvertToViewModel(TreatmentBooking booking)
        {
            var viewModel = new TreatmentBookingViewModel
            {
                BookingId = booking.BookingId,
                UserId = booking.UserId,
                DoctorId = booking.DoctorId,
                ServiceId = booking.ServiceId,
                BookingDate = booking.BookingDate,
                Status = booking.Status ?? "N/A"
            };

            // Get user information
            if (booking.User != null)
            {
                viewModel.UserName = booking.User.FullName ?? "N/A";
                viewModel.UserEmail = booking.User.Email ?? "N/A";
                viewModel.UserPhone = booking.User.PhoneNumber ?? "N/A";
            }

            // Get doctor information
            if (booking.Doctor?.User != null)
            {
                viewModel.DoctorName = booking.Doctor.User.FullName ?? "N/A";
                viewModel.DoctorSpecialization = booking.Doctor.Specialization ?? "N/A";
            }

            // Get service information
            if (booking.Service != null)
            {
                viewModel.ServiceName = booking.Service.Name ?? "N/A";
                viewModel.ServiceMethodType = booking.Service.MethodType ?? "N/A";
                viewModel.ServicePrice = booking.Service.Price;
                viewModel.ServiceDescription = booking.Service.Description ?? "N/A";
            }

            return viewModel;
        }
    }
}
