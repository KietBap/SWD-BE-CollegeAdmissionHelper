using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.OpenApi.Models;
using CAH.API;
using CAH.Repositories.Mapper;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Services.AddHttpClient();
builder.Services.AddControllers();
builder.Services.AddSignalR();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "College Admission Helper API", Version = "v1" });
    // Document Swagger
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    option.IncludeXmlComments(xmlPath);

    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

	option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins(
            "http://localhost:4200",
            "http://localhost:5173",
            "https://swdfe-9pgc.vercel.app"
        )
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
    });

    options.AddPolicy("AllowSwagger", policy =>
    {
        policy.WithOrigins("https://swpproject-egd0b4euezg4akg7.southeastasia-01.azurewebsites.net")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


builder.Services.AddConfig(builder.Configuration);
builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    options.JsonSerializerOptions.WriteIndented = true;
});


FirebaseApp.Create(new AppOptions()
{
    Credential = GoogleCredential.FromFile("collegeadmissionhelper-firebase-adminsdk-fbsvc-7898fc81a5.json")
});


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();
// Enable CORS
app.UseCors(); // Để áp dụng DefaultPolicy cho frontend

if (app.Environment.IsDevelopment())
{
    app.UseCors("AllowSwagger"); // Chỉ mở CORS cho Swagger khi dev
}


app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();


app.MapControllers();



app.Run();
