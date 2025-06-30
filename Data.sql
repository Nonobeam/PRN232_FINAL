-- Use the database
USE InfertilityTreatmentDB;
GO

INSERT INTO Roles (Name) VALUES
(N'Admin'),
(N'Manager'),
(N'Doctor'),
(N'Customer');

-- Khách hàng (pass: 123456)
INSERT INTO Users (RoleId, FullName, Email, PhoneNumber, Gender, DOB, PasswordHash)
VALUES 
(4, N'Nguyễn Thị A', 'user1@gmail.com', '0912345678', N'Nữ', '1990-05-20', '1ZirBhtmtV21yx1u7cf7PJlSrstaNvQPIy7vTcjXs95kPS3qx6koyoeGspnF+MM7'),
(4, N'Trần Văn B', 'user2@gmail.com', '0987654321', N'Nam', '1985-11-15', '1ZirBhtmtV21yx1u7cf7PJlSrstaNvQPIy7vTcjXs95kPS3qx6koyoeGspnF+MM7');

-- Bác sĩ
INSERT INTO Users (RoleId, FullName, Email, PhoneNumber, Gender, DOB, PasswordHash)
VALUES 
(3, N'BS. Lê Văn C', 'doctor1@gmail.com', '0901234567', N'Nam', '1975-08-10', '1ZirBhtmtV21yx1u7cf7PJlSrstaNvQPIy7vTcjXs95kPS3qx6koyoeGspnF+MM7'),
(3, N'BS. Nguyễn Thị D', 'doctor2@gmail.com', '0909876543', N'Nữ', '1980-03-25', '1ZirBhtmtV21yx1u7cf7PJlSrstaNvQPIy7vTcjXs95kPS3qx6koyoeGspnF+MM7');

-- Quản lý & Admin
INSERT INTO Users (RoleId, FullName, Email, PhoneNumber, Gender, DOB, PasswordHash)
VALUES
(2, N'Ngô Quản Lý', 'manager@example.com', '0933333333', N'Nam', '1982-06-15', '1ZirBhtmtV21yx1u7cf7PJlSrstaNvQPIy7vTcjXs95kPS3qx6koyoeGspnF+MM7'),
(1, N'Phạm Quản Trị', 'admin@example.com', '0944444444', N'Nữ', '1979-01-05', '1ZirBhtmtV21yx1u7cf7PJlSrstaNvQPIy7vTcjXs95kPS3qx6koyoeGspnF+MM7');

-- Giả định UserId 3 và 4 là bác sĩ
INSERT INTO Doctors (UserId, Specialization, Degree, YearsOfExperience, WorkSchedule)
VALUES 
(3, N'Hiếm muộn - IVF', N'Tiến sĩ Y học', 15, N'T2-T6: 08:00 - 17:00'),
(4, N'Điều trị IUI', N'Thạc sĩ Sản khoa', 10, N'T3-T7: 08:00 - 16:30');

INSERT INTO Services (Name, Description, MethodType, Price)
VALUES
(N'IUI - Thụ tinh trong tử cung', N'Phương pháp bơm tinh trùng vào tử cung', N'IUI', 15000000),
(N'IVF - Thụ tinh trong ống nghiệm', N'Phương pháp thụ tinh trong ống nghiệm hiện đại', N'IVF', 70000000),
(N'Tư vấn chuyên sâu', N'Tư vấn điều trị và xét nghiệm trước điều trị', N'Tư vấn', 500000);

-- Giả định UserId 1 là khách hàng, DoctorId 1 là bác sĩ
INSERT INTO TreatmentBookings (UserId, DoctorId, ServiceId, Status)
VALUES
(1, 1, 1, N'Đã đặt lịch'),
(2, 2, 2, N'Chờ xác nhận');

INSERT INTO TreatmentSchedules (BookingId, EventDate, EventType, Description)
VALUES
(1, '2025-07-01 08:00', N'Tiêm thuốc', N'Bắt đầu tiêm hormone'),
(1, '2025-07-05 09:00', N'Xét nghiệm', N'Xét nghiệm nội tiết tố'),
(2, '2025-07-10 10:00', N'Tư vấn', N'Tư vấn IVF lần đầu');

INSERT INTO MedicalRecords (BookingId, VisitDate, Notes, TestResults, DoctorId)
VALUES
(1, '2025-07-01', N'Không có dấu hiệu bất thường', N'Nội tiết ổn định', 1),
(2, '2025-07-10', N'Cần theo dõi thêm', N'Hormone FSH cao', 2);

INSERT INTO Feedbacks (UserId, DoctorId, Rating, Comment)
VALUES
(1, 1, 5, N'Bác sĩ rất tận tâm và chuyên nghiệp'),
(2, 2, 4, N'Dịch vụ tốt, cần cải thiện thời gian chờ');

INSERT INTO BlogPosts (Title, Content, CreatedBy)
VALUES
(N'Kinh nghiệm điều trị IUI thành công', N'Tôi đã trải qua 3 lần IUI và thành công lần thứ 3...', 1),
(N'IVF có đau không?', N'Nhiều người hỏi IVF có đau không, dưới đây là trải nghiệm của tôi...', 2);
