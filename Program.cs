using Amazon.Runtime.Credentials;
using CoScheduleOA.Configurations;
using CoScheduleOA.Domain.Context;
using CoScheduleOA.Infrastructure.Models;
using CoScheduleOA.Interfaces.Providers;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.ConfigureOptions(builder.Configuration);
builder.Services.ConfigureAwsServices(builder.Configuration);
builder.Services.ConfigureRepositories();
builder.Services.ConfigureServices();
builder.Services.ConfigureJwt(builder.Configuration);
builder.Services.AddRedditClients(builder.Configuration);

builder.Services.AddDbContextFactory<CoScheduleOAContext>((sp, options) =>
{
    var connBuilder = sp.GetRequiredService<IConnectionStringProvider>();
    var cfg = sp.GetRequiredService<IOptions<AwsSecretOptions>>().Value;
    var conn = connBuilder.GetPostgresConnectionString(cfg.DbSecretId);
    options.UseNpgsql(conn);
});

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "CoScheduleOA API",
        Version = "v1"
    });

    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Enter 'Bearer' followed by space and token",
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        if (builder.Environment.IsDevelopment())
        {
            policy
                .WithOrigins("http://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        }
        else
        {
            policy
                .WithOrigins(
                    "https://coschedule-client.vercel.app",
                    "https://jk-coschedule-oa.com",
                    "https://www.jk-coschedule-oa.com"
                )
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        }
    });
});

builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseExceptionHandler(appError =>
{
    appError.Run(async context =>
    {
        context.Response.ContentType = "application/json";

        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

        if (exception is Npgsql.PostgresException pgEx && pgEx.SqlState == "28P01")
        {
            var connStringProvider = context.RequestServices.GetRequiredService<IConnectionStringProvider>();
            var secretOptions = context.RequestServices.GetRequiredService<IOptions<AwsSecretOptions>>().Value;

            connStringProvider.ForceRefresh(secretOptions.DbSecretId);

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(new
            {
                StatusCode = context.Response.StatusCode,
                Message = "Database authentication failed. Secrets refreshed. Please retry."
            });
            return;
        }

        context.Response.StatusCode = exception switch
        {
            UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
            ArgumentException => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };

        var errorResponse = new
        {
            StatusCode = context.Response.StatusCode,
            Message = exception?.Message ?? "An unexpected error occurred."
        };

        await context.Response.WriteAsJsonAsync(errorResponse);
    });
});


//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();
app.UseCors("AllowFrontend");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
