using Service;
using Service.Implement;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddScoped<IUserRepository, UserRepository>();
//builder.Services.AddScoped<IRoleRepository, RoleRepository>();
//builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
//builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
//builder.Services.AddScoped<ITreatmentBookingRepository, TreatmentBookingRepository>();
//builder.Services.AddScoped<ITreatmentScheduleRepository, TreatmentScheduleRepository>();
//builder.Services.AddScoped<IMedicalRecordRepository, MedicalRecordRepository>();
//builder.Services.AddScoped<IFeedbackRepository, FeedbackRepository>();
//builder.Services.AddScoped<IBlogPostRepository, BlogPostRepository>();


builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<IServicesService, ServicesService>();
builder.Services.AddScoped<ITreatmentBookingService, TreatmentBookingService>();
builder.Services.AddScoped<ITreatmentScheduleService, TreatmentScheduleService>();
builder.Services.AddScoped<IMedicalRecordService, MedicalRecordService>();
builder.Services.AddScoped<IFeedbackService, FeedbackService>();
builder.Services.AddScoped<IBlogPostService, BlogPostService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();