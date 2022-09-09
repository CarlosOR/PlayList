using Microsoft.EntityFrameworkCore;
using PlayListProject.Domain.Context;
using PlayListProject.Presentation.Middleware;
using PlayListProject.Presentation.Resolvers;
using PlayListProject.Presentation.Resolvers.Domain;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Inyec of Dependencies
builder.Services.ResolveDependencyInjection();
builder.Services.ResolveContext(builder.Configuration.GetValue<string>("SqliteUrlConnection"));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();
app.UseMiddleware<GlobalExceptionHandler>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//Run migrations automatic
using (var scope = app.Services.CreateScope())
{
    PlayListProjectDbContext db = scope.ServiceProvider.GetRequiredService<PlayListProjectDbContext>();
    db.Database.Migrate();
}

app.Run();


