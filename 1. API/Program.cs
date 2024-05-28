using _1._API.Mapper;
using _2._Domain;
using _3._Data;
using _3._Data.Context;
using _3._Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Cors;

var builder = WebApplication.CreateBuilder(args);

// Configurar el puerto explícitamente si se proporciona la variable de entorno PORT
var port = Environment.GetEnvironmentVariable("PORT") ?? "80";
builder.WebHost.UseUrls($"http://*:{port}");

// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "StudyMentor API",
        Description = "API to manage data for studymentor",
    });
});
//Inyeccion dependencias
builder.Services.AddScoped<IPaymentData, PaymentMySqlData>();
builder.Services.AddScoped<IPaymentDomain, PaymentDomain>();
builder.Services.AddScoped<IStudentData, StudentSQLData>();
builder.Services.AddScoped<IStudentDomain, StudentDomain>();
builder.Services.AddScoped<ITutorData, TutorSQLData>();
builder.Services.AddScoped<ITutorDomain, TutorDomain>();
builder.Services.AddScoped<IScoreData, ScoreMySqlData>();
builder.Services.AddScoped<IScoreDomain, ScoreDomain>();
builder.Services.AddScoped<IReviewData, ReviewMySqlData>();
builder.Services.AddScoped<IReviewDomain, ReviewDomain>();

builder.Services.AddScoped<IScheduleData, ScheduleSQLData>();
builder.Services.AddScoped<IScheduleDomain, ScheduleDomain>();

//Pomelo MySql Connection
var connectionString = builder.Configuration.GetConnectionString("sql10709845");
builder.Services.AddDbContext<StudyMentorDB>(
    dbContextOptions =>
    {
        dbContextOptions.UseMySql(connectionString,
            ServerVersion.AutoDetect(connectionString),
            options => options.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: System.TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null)
        );
    });

//Automapper
builder.Services.AddAutoMapper(
    typeof(ModelToAPI),
    typeof(APIToModel)
);
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<StudyMentorDB>();
    context.Database.Migrate();
}

// Configure the HTTP request pipeline.
/*if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}*/

// Configurar el pipeline de solicitudes HTTP
app.UseSwagger();
app.UseSwaggerUI(c => 
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "StudyMentor API V1");
});

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors(builder =>
{
    builder.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
});

app.MapControllers();

app.Run();