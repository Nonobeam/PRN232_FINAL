namespace WebAPI.DTO
{
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

    public class ServiceDTO
    {
        public int ServiceId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string MethodType { get; set; } = string.Empty;
        public decimal? Price { get; set; }
        public bool? IsActive { get; set; }
    }

    public class TreatmentBookingDTO
    {
        public int BookingId { get; set; }
        public int? UserId { get; set; }
        public int? DoctorId { get; set; }
        public int? ServiceId { get; set; }
        public DateTime? BookingDate { get; set; }
        public string Status { get; set; } = string.Empty;

        // Related entity information
        public DoctorDTO? Doctor { get; set; }
        public ServiceDTO? Service { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
    }

    public class UpdateStatusRequest
    {
        public string Status { get; set; } = string.Empty;
    }
}
