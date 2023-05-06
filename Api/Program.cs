using Api.Helpers;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCustomConfiguration(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseFileServer(new FileServerOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath + @".\..\logic\wwwroot")),
    EnableDirectoryBrowsing = true
});
// app.UseStaticFiles(new StaticFileOptions
// {
//     FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath + @".\..\logic\wwwroot")),
//     ContentTypeProvider = new FileExtensionContentTypeProvider(new Dictionary<string, string>()
//     {
//         { "py", "text/x-python" }
//     })
// });

app.UseCors("allowLocalAtPort5173");

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();