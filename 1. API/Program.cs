using System.Text;
using _1._API.Mapper;
using _2._Domain;
using _3._Data;
using _3._Data.Context;
using _3._Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configurar el puerto explícitamente si se proporciona la variable de entorno PORT
var port = Environment.GetEnvironmentVariable("PORT") ?? "80";
builder.WebHost.UseUrls($"http://*:{port}");

// Add services to the container.

// Configurar logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Logging.AddEventSourceLogger();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

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
    
    // Configurar Swagger para usar autenticación JWT
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
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

builder.Services.AddScoped<IAuthDomain, AuthDomain>();

//Pomelo MySql Connection
var connectionString = builder.Configuration.GetConnectionString("MysqlConection");
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


// Configurar el pipeline de solicitudes HTTP

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseSwagger();
app.UseSwaggerUI(c => 
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "StudyMentor API V1");
});

//app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();
app.UseCors(builder =>
{
    builder.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
});

app.MapControllers();

app.Run();
