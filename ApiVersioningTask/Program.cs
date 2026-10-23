using Asp.Versioning;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// 1. إعداد الـ Versioning والـ ApiExplorer
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = false;
    options.ReportApiVersions = true;
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
})
.AddMvc()
.AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV"; // سيظهر كـ v1 و v2
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddControllers();

// 2. إعداد SwaggerGen لإنشاء مستندين
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // تعريف الإصدار الأول
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    // تعريف الإصدار الثاني
    options.SwaggerDoc("v2", new OpenApiInfo { Title = "My API", Version = "v2" });
});

var app = builder.Build();

// 3. إعداد واجهة Swagger UI
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        // إضافة المسارات للمستندين
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
        options.SwaggerEndpoint("/swagger/v2/swagger.json", "API V2");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();