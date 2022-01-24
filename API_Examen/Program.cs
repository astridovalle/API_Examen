using API_Examen;
using API_Examen.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IActivity, ActivityRepo>();

builder.Services.AddDbContext<ActivitiesDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("ActivitiesCon")));


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
