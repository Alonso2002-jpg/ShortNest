using Context.Mappers;
using Controller.Utils;
using Microsoft.EntityFrameworkCore;
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

// Agrega CORS a los servicios
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder
                .WithOrigins("*") // Asegúrate de reemplazar esto con la URL de tu cliente
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Apply Execute Scipts
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var scriptExecutor = services.GetRequiredService<ExecuteScripts>();
    var scriptDirectory = app.Environment.IsDevelopment() ? Path.Combine(Directory.GetCurrentDirectory(),"scripts") : Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "..", "scripts");
    scriptExecutor.ExecuteSQLFiles(scriptDirectory);
}
{
    
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowSpecificOrigin");

app.UseMiddleware<ExceptionMiddleware>();
app.MapControllers();
app.Run();