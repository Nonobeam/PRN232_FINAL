USE master
GO

-- Create the database
CREATE DATABASE InfertilityTreatmentDB;
GO

-- Use the database
USE InfertilityTreatmentDB;
GO

-- Roles
CREATE TABLE Roles (
    RoleId INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(50) UNIQUE NOT NULL
);

-- Users
CREATE TABLE Users (
    UserId INT PRIMARY KEY IDENTITY,
    RoleId INT NOT NULL FOREIGN KEY REFERENCES Roles(RoleId),
    FullName NVARCHAR(100),
    Email NVARCHAR(100) UNIQUE,
    PhoneNumber NVARCHAR(15),
    Gender NVARCHAR(10),
    DOB DATE,
    PasswordHash NVARCHAR(255),
    CreatedAt DATETIME DEFAULT GETDATE()
);

-- Doctors
CREATE TABLE Doctors (
    DoctorId INT PRIMARY KEY IDENTITY,
    UserId INT UNIQUE FOREIGN KEY REFERENCES Users(UserId),
    Specialization NVARCHAR(100),
    Degree NVARCHAR(100),
    YearsOfExperience INT,
    WorkSchedule NVARCHAR(255)
);

-- Services
CREATE TABLE Services (
    ServiceId INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(100),
    Description NVARCHAR(MAX),
    MethodType NVARCHAR(20),
    Price DECIMAL(18,2),
    IsActive BIT DEFAULT 1
);

-- Treatment Bookings
CREATE TABLE TreatmentBookings (
    BookingId INT PRIMARY KEY IDENTITY,
    UserId INT FOREIGN KEY REFERENCES Users(UserId),
    DoctorId INT FOREIGN KEY REFERENCES Doctors(DoctorId),
    ServiceId INT FOREIGN KEY REFERENCES Services(ServiceId),
    BookingDate DATETIME DEFAULT GETDATE(),
    Status NVARCHAR(50)
);

-- Treatment Schedules
CREATE TABLE TreatmentSchedules (
    ScheduleId INT PRIMARY KEY IDENTITY,
    BookingId INT FOREIGN KEY REFERENCES TreatmentBookings(BookingId),
    EventDate DATETIME,
    EventType NVARCHAR(100),
    Description NVARCHAR(MAX),
    Reminder BIT DEFAULT 1
);

-- Medical Records
CREATE TABLE MedicalRecords (
    RecordId INT PRIMARY KEY IDENTITY,
    BookingId INT FOREIGN KEY REFERENCES TreatmentBookings(BookingId),
    VisitDate DATETIME,
    Notes NVARCHAR(MAX),
    TestResults NVARCHAR(MAX),
    DoctorId INT FOREIGN KEY REFERENCES Doctors(DoctorId)
);

-- Feedbacks
CREATE TABLE Feedbacks (
    FeedbackId INT PRIMARY KEY IDENTITY,
    UserId INT FOREIGN KEY REFERENCES Users(UserId),
    DoctorId INT FOREIGN KEY REFERENCES Doctors(DoctorId),
    Rating INT CHECK (Rating BETWEEN 1 AND 5),
    Comment NVARCHAR(MAX),
    CreatedAt DATETIME DEFAULT GETDATE()
);

-- Blog Posts
CREATE TABLE BlogPosts (
    PostId INT PRIMARY KEY IDENTITY,
    Title NVARCHAR(255),
    Content NVARCHAR(MAX),
    CreatedBy INT FOREIGN KEY REFERENCES Users(UserId),
    CreatedAt DATETIME DEFAULT GETDATE()
);
GO

-- Dashboard View (example)
CREATE VIEW vw_TreatmentStats AS
SELECT
    S.MethodType,
    COUNT(*) AS TotalTreatments,
    AVG(F.Rating) AS AverageRating
FROM TreatmentBookings TB
JOIN Services S ON TB.ServiceId = S.ServiceId
LEFT JOIN Feedbacks F ON TB.DoctorId = F.DoctorId
GROUP BY S.MethodType;
