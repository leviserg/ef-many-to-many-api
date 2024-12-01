using Microsoft.EntityFrameworkCore;
using students_courses_api.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var dbLogin = Environment.GetEnvironmentVariable("DEVELOPER_LOGIN") ?? string.Empty;
var dbPassword = Environment.GetEnvironmentVariable("DEVELOPER_PWD") ?? string.Empty;
var dbName = Environment.GetEnvironmentVariable("DEVELOPER_DB") ?? string.Empty;

var connectionString = string.Format(builder.Configuration.GetConnectionString("sqlserver") ?? string.Empty, dbLogin, dbPassword, dbName);

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

builder.Services.AddScoped<DbSeed>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using(var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        db.Database.EnsureCreated();
        if (!db.StudentCourses.Any())
        {
            var seeder = scope.ServiceProvider.GetRequiredService<DbSeed>();
            seeder.Seed();
        }
    }


}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
