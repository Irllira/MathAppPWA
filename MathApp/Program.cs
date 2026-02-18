using MathApp.Backend.API.Interfaces;
using MathApp.Backend.API.Repos;
using MathApp.Backend.Data.Enteties;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DataBase>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default Connection")));


builder.Services.AddScoped<IAccountRepo, AccountRepo>();
builder.Services.AddScoped<IEducationLevelRepo, EducationLevelRepo>();
builder.Services.AddScoped<IUnitRepo, UnitRepo>();
builder.Services.AddScoped<IDefinitionRepo, DefinitionRepo>();
builder.Services.AddScoped<IIncorrectDefinitionRepo, IncorrectDefinitionRepo>();
builder.Services.AddScoped<IPagesRepo, PagesRepo>();
builder.Services.AddScoped<IUserProgressRepo, UserProgressRepo>();




builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors(policy => policy.WithOrigins("https://localhost:4000","http://localhost:4001").AllowAnyMethod().WithHeaders(HeaderNames.ContentType));

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//Seeding the database
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;


try
{
    var context = services.GetRequiredService<DataBase>();
    await context.Database.MigrateAsync();

    await DataSeeder.Seed(context);
}
catch (Exception e)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(e, "Any error occured during migration / seeding");
}

app.Run();
