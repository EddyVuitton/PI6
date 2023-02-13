using PI6.WebApi.Data;
using PI6.WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
AddServices(builder);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

static void AddServices(WebApplicationBuilder builder)
{
    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddWebAPI();
    builder.Services.AddSqlServer<DBContext>(builder.Configuration.GetConnectionString("DefaultConnection"));
}