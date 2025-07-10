using System.ComponentModel.DataAnnotations;

namespace WebApp.Attributes
{
    public class BusinessHoursValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is DateTime dateTime)
            {
                // Check if it's Monday to Friday (1-5, where Sunday = 0)
                var dayOfWeek = (int)dateTime.DayOfWeek;
                if (dayOfWeek == 0 || dayOfWeek == 6) // Sunday or Saturday
                {
                    ErrorMessage = "Chỉ có thể đặt lịch từ thứ 2 đến thứ 6";
                    return false;
                }

                // Check if it's between 8:00 AM and 5:00 PM
                var time = dateTime.TimeOfDay;
                var startTime = new TimeSpan(8, 0, 0); // 8:00 AM
                var endTime = new TimeSpan(17, 0, 0);  // 5:00 PM
                var lunchStart = new TimeSpan(12, 0, 0); // 12:00 PM
                var lunchEnd = new TimeSpan(13, 0, 0);   // 1:00 PM

                if (time < startTime || time >= endTime)
                {
                    ErrorMessage = "Giờ đặt lịch phải từ 8:00 đến 17:00";
                    return false;
                }

                // Check if it's during lunch break
                if (time >= lunchStart && time < lunchEnd)
                {
                    ErrorMessage = "Không thể đặt lịch trong giờ nghỉ trưa (12:00 - 13:00)";
                    return false;
                }

                // Check if it's not in the past
                if (dateTime <= DateTime.Now)
                {
                    ErrorMessage = "Ngày đặt lịch phải là ngày trong tương lai";
                    return false;
                }

                return true;
            }

            return false;
        }
    }

    public class WeekdayValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is DateTime dateTime)
            {
                var dayOfWeek = (int)dateTime.DayOfWeek;
                if (dayOfWeek == 0 || dayOfWeek == 6) // Sunday or Saturday
                {
                    ErrorMessage = "Chỉ có thể đặt lịch từ thứ 2 đến thứ 6";
                    return false;
                }

                // Check if it's not in the past
                if (dateTime.Date <= DateTime.Today)
                {
                    ErrorMessage = "Ngày đặt lịch phải là ngày trong tương lai";
                    return false;
                }

                return true;
            }

            return false;
        }
    }

    public class BusinessTimeValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is TimeSpan time)
            {
                var startTime = new TimeSpan(8, 0, 0); // 8:00 AM
                var endTime = new TimeSpan(17, 0, 0);  // 5:00 PM
                var lunchStart = new TimeSpan(12, 0, 0); // 12:00 PM
                var lunchEnd = new TimeSpan(13, 0, 0);   // 1:00 PM

                if (time < startTime || time >= endTime)
                {
                    ErrorMessage = "Giờ đặt lịch phải từ 8:00 đến 17:00";
                    return false;
                }

                // Check if it's during lunch break
                if (time >= lunchStart && time < lunchEnd)
                {
                    ErrorMessage = "Không thể đặt lịch trong giờ nghỉ trưa (12:00 - 13:00)";
                    return false;
                }

                return true;
            }

            return false;
        }
    }
}
