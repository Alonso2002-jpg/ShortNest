using System.Text;
using Context.Mappers;
using Context.Models;
using Controller.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Services.Services;
using Services.Utils;
using AppContext = Context.Context.AppContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppContext>( options =>
    options.UseNpgsql(connString,b => b.MigrationsAssembly("Context"))
    );

builder.Services.AddTransient<ExecuteScripts>();
builder.Services.AddScoped<UrlStorageService>();
builder.Services.AddScoped<UrlStorageMapper>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<UserMapper>();
builder.Services.AddScoped<UserRoleService>();
builder.Services.AddScoped<UserRoleMapper>();
builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<AuthService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});
// Agrega CORS a los servicios
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder
                .WithOrigins("http://localhost:5173")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opts =>
{
    opts.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    opts.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Por favor, ingresa el token con 'Bearer ' antes del token",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    opts.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});

var app = builder.Build();

// Apply Execute Scipts
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var scriptExecutor = services.GetRequiredService<ExecuteScripts>();
    var scriptDirectory = app.Environment.IsDevelopment() ? Path.Combine(Directory.GetCurrentDirectory(),"scripts") : Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "..", "scripts");
    scriptExecutor.ExecuteSQLFiles(scriptDirectory);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowSpecificOrigin");
app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ExceptionMiddleware>();
app.MapControllers();
app.Run();