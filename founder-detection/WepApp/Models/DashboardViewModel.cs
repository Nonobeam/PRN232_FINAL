using Model.Models;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class DashboardViewModel
    {
        public string UserRole { get; set; } = string.Empty;
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;

        // Admin Dashboard Data
        public int TotalUsers { get; set; }
        public int TotalDoctors { get; set; }
        public int TotalBookings { get; set; }
        public int TotalServices { get; set; }

        // Doctor Dashboard Data
        public int MyAppointments { get; set; }
        public int MyMedicalRecords { get; set; }
        public int TodayAppointments { get; set; }

        // Customer Dashboard Data
        public int MyBookings { get; set; }
        public int UpcomingSchedules { get; set; }
        public int CompletedTreatments { get; set; }

        // Manager Dashboard Data
        public int PendingBookings { get; set; }
        public int ActiveTreatments { get; set; }
        public decimal MonthlyRevenue { get; set; }

        // Common Dashboard Data
        public List<RecentActivity> RecentActivities { get; set; } = new List<RecentActivity>();
        public List<QuickAction> QuickActions { get; set; } = new List<QuickAction>();
    }

    public class BookTreatmentViewModel
    {
        [Required(ErrorMessage = "Vui lòng chọn dịch vụ điều trị")]
        [Display(Name = "Dịch vụ điều trị")]
        public int ServiceId { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn bác sĩ")]
        [Display(Name = "Bác sĩ điều trị")]
        public int DoctorId { get; set; }

        [Display(Name = "Ghi chú")]
        [StringLength(500, ErrorMessage = "Ghi chú không được vượt quá 500 ký tự")]
        public string Notes { get; set; } = string.Empty;

        // Data for dropdowns
        public List<Services> AvailableServices { get; set; } = new List<Services>();
        public List<DoctorViewModel> AvailableDoctors { get; set; } = new List<DoctorViewModel>();

        // Current user info
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
    }

    public class DoctorViewModel
    {
        public int DoctorId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;
        public string Degree { get; set; } = string.Empty;
        public int? YearsOfExperience { get; set; }
        public string WorkSchedule { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
    }

    // DTO class for API communication
    public class DoctorDTO
    {
        public int DoctorId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;
        public string Degree { get; set; } = string.Empty;
        public int? YearsOfExperience { get; set; }
        public string WorkSchedule { get; set; } = string.Empty;
    }

    public class RecentActivity
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string Type { get; set; } = string.Empty; // booking, schedule, record, etc.
        public string Icon { get; set; } = string.Empty;
    }

    public class QuickAction
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Controller { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public string CssClass { get; set; } = "btn-primary";
    }
}
