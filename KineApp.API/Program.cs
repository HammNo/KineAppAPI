using KineApp.BLL.DependencyInjection;
using KineApp.DAL.Contexts;
using KineApp.DAL.DependencyInjection;
using KineApp.IL.Configurations;
using KineApp.IL.DependencyInjection;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<KineAppContext>(
    o => o.UseSqlServer(builder.Configuration.GetConnectionString("Default")
));
builder.Services.AddRepositories();
builder.Services.AddBusinessServices();
builder.Services.AddControllers();

MailerConfig mailerConfig = builder.Configuration.GetSection("Smtp").Get<MailerConfig>();
builder.Services.AddMailer(mailerConfig);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(builder =>
{
    builder.AddDefaultPolicy(o =>
    {
        o.AllowAnyOrigin();
        o.AllowAnyHeader();
        o.AllowAnyMethod();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
