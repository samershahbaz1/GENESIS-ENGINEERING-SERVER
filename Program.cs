using Microsoft.EntityFrameworkCore;
using WebApplication1.Model.DataModels;
using WebApplication1.Repo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>((options) =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddHttpClient();
builder.Services.AddScoped<UniversityRepository>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(option => option.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
app.UseAuthorization();

app.MapControllers();

app.Run();
