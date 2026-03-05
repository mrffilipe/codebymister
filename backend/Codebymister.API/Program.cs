using Asp.Versioning;
using Codebymister.API.Middleware;
using Codebymister.Infrastructure.Configurations.Auth;
using Codebymister.Infrastructure.Configurations.DbContext;
using Codebymister.Infrastructure.Configurations.Firebase;
using Codebymister.Infrastructure.Extensions;
using Codebymister.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services
            .AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "CodeByMister API",
                Version = "v1",
                Description = "API para gerenciamento do CodeByMister"
            });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Insira o token JWT no campo abaixo. Exemplo: Bearer {seu token}"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });

        builder.Services
            .AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            })
            .AddMvc()
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

        builder.Services
            .AddServices(builder.Configuration)
            .AddUseCases()
            .AddRepositories()
            .AddQueries();

        builder.Services
            .AddApplicationDbContext(builder.Configuration)
            .AddAuthConfigurations(builder.Configuration)
            .AddFirebaseConfigurations(builder.Configuration)
            .AddCors(options =>
            {
                options.AddPolicy("AllowAnyOrigin", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

        builder.Services.AddScoped<UserScopeMiddleware>();

        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            db.Database.Migrate();
        }

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors("AllowAnyOrigin");

        app.UseAuthentication();
        app.UseMiddleware<UserScopeMiddleware>();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
