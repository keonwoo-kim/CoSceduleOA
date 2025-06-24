using Amazon.SecretsManager;
using CoScheduleOA.Domain;
using CoScheduleOA.Infrastructure;
using CoScheduleOA.Repositories;
using CoScheduleOA.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());
builder.Services.AddAWSService<IAmazonSecretsManager>();

builder.Services.Configure<DbSettings>(
    builder.Configuration.GetSection("Database"));

builder.Services.Configure<AwsSecretManagerOptions>(
    builder.Configuration.GetSection("AwsSecretManagerOptions"));



builder.Services.AddSingleton<AwsSecretManager>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<ILoginRepository, LoginRepository>();


builder.Services.AddDbContext<CoSceduleOAContext>((sp, opt) =>
{
    var asm = sp.GetRequiredService<AwsSecretManager>();
    var conn = asm.BuildPgConnectionString(
                   sp.GetRequiredService<IOptions<AwsSecretManagerOptions>>()
                     .Value.SecretId);
    opt.UseNpgsql(conn);
});

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
