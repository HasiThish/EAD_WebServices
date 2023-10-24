using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System.Text;
using WebService.Models;
using WebService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<MongoDBSettings>(
    builder.Configuration.GetSection(nameof(MongoDBSettings)));

builder.Services.AddSingleton<IMongoDBSettings>(sp =>
    sp.GetRequiredService<IOptions<MongoDBSettings>>().Value);

builder.Services.AddSingleton<IMongoClient>(s =>
new MongoClient(builder.Configuration.GetValue<string>("MongoDBSettings:ConnectionString")));

builder.Services.AddCors();

builder.Services.AddScoped<ITrainService, TrainService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<ITravellerService, TravellerService>();
builder.Services.AddScoped<IBookingService, BookingService>();

//builder.Services.AddCors(options =>
//{
//    //allow frontend url
//    options.AddPolicy("MyCorsPolicy",
//        builder => builder.WithOrigins("http://localhost:3000")
//                           .AllowAnyHeader()
//                           .AllowAnyMethod());

//    options.AddPolicy("MyCorsPolicy2",
//        builder => builder.WithOrigins("http://localhost:")
//                           .AllowAnyHeader()
//                           .AllowAnyMethod());
//});

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
}

//app.UseCors("MyCorsPolicy");

//app.UseCors("MyCorsPolicy2");

app.UseCors(builder =>
{
    builder.AllowAnyOrigin()    // Allow requests from any origin
           .AllowAnyMethod()    // Allow any HTTP method (GET, POST, PUT, DELETE, etc.)
           .AllowAnyHeader();    // Allow any header
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
