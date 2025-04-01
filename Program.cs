using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Worthy_API.Data;
using Worthy_API.Interfaces;
using Worthy_API.Repository;

var builder = WebApplication.CreateBuilder(args);

// ✅ 1️⃣ CORS Configuration (More Secure)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", policy =>
    {
        policy.WithOrigins("https://yourfrontend.com") // Change this to your actual frontend domain
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

// ✅ 2️⃣ Add Services to Container
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });

// ✅ 3️⃣ Database Configuration
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// ✅ 4️⃣ Dependency Injection (DI)
builder.Services.AddScoped<IMetricsRepository, MetricsRepository>();

// ✅ 5️⃣ Swagger Configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
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
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

var app = builder.Build();

// ✅ 6️⃣ Middleware Configuration
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Worthy API v1"));
}

// Enable CORS globally
app.UseCors("AllowSpecificOrigins");

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// ✅ 7️⃣ Authorization & Exception Handling
app.UseAuthorization();
// app.UseMiddleware<CustomExceptionMiddleware>(); // Optional: Add custom error handling middleware

// ✅ 8️⃣ Default MVC Route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();