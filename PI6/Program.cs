using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using MudBlazor.Services;
using PI6.Server.Services;
using PI6.WebApi.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
AddServices(builder);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

var filesDirectory = ServerHelper.GetFilesDirectory();
if (!filesDirectory.IsNullOrEmpty())
{
    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(ServerHelper.GetFilesDirectory()),
        RequestPath = "/files"
    });
}

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();

static void AddServices(WebApplicationBuilder builder)
{
    builder.Services.AddRazorPages();
    builder.Services.AddServerSideBlazor();
    builder.Services.AddMudServices();
    builder.Services.AddPI6();
}