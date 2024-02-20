using Backend.Services;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddSingleton<IPeopleService, PeopleTwoService>(); Esto es inyección de dependencias
builder.Services.AddKeyedSingleton<IPeopleService, PeopleService>("peopleService"); //Tambien se pueden usar keys en caso de tener varias dependencias
builder.Services.AddKeyedSingleton<IPeopleService, PeopleTwoService>("peopleTwoService");

builder.Services.AddKeyedSingleton<IRandomService, RandomService>("randomSingleton");
builder.Services.AddKeyedScoped<IRandomService, RandomService>("randomScoped");
builder.Services.AddKeyedTransient<IRandomService, RandomService>("randomTransient");

builder.Services.AddScoped<IPostService, PostService>();

builder.Services.AddHttpClient<IPostService, PostService>(c =>
{
    c.BaseAddress = new Uri(builder.Configuration["BaseUrlPost"]);
});

builder.Services.AddControllers();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
