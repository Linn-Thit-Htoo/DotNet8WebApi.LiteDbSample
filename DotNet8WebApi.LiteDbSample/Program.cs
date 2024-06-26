using DotNet8WebApi.LiteDbSample.Services;
using LiteDB;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.PropertyNamingPolicy = null;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped(n =>
{
    var _folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LiteDb");
    Directory.CreateDirectory(_folderPath);
    var _filePath = Path.Combine(_folderPath, "Blog.db");

    return new LiteDatabase(_filePath);
});

builder.Services.AddScoped<LiteDbV2Service>();
builder.Services.AddScoped<LiteDbV3Service>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();