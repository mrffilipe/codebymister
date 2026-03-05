using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Codebymister.Infrastructure.Configurations.Firebase;

public static class FirebaseConfiguration
{
    public static IServiceCollection AddFirebaseConfigurations(
    this IServiceCollection services,
    IConfiguration configuration)
    {
        var base64 = configuration["FIREBASE_SERVICE_ACCOUNT_BASE64"];

        var projectId = configuration["Firebase:ProjectId"]
            ?? throw new InvalidOperationException("Firebase:ProjectId não configurado.");

        var environment = configuration["ASPNETCORE_ENVIRONMENT"]
            ?? throw new InvalidOperationException("ASPNETCORE_ENVIRONMENT não configurado.");

        GoogleCredential credential;

        if (!string.IsNullOrWhiteSpace(base64))
        {
            var json = Encoding.UTF8.GetString(Convert.FromBase64String(base64));

            var serviceAccount = JsonSerializer.Deserialize<ServiceAccountKey>(json)
                ?? throw new InvalidOperationException("ServiceAccount inválido.");

            if (string.IsNullOrWhiteSpace(serviceAccount.PrivateKey))
                throw new InvalidOperationException("Firebase private_key não configurada.");

            if (string.IsNullOrWhiteSpace(serviceAccount.ClientEmail))
                throw new InvalidOperationException("Firebase client_email não configurado.");

            var initializer = new ServiceAccountCredential.Initializer(serviceAccount.ClientEmail)
            {
                Scopes = new[]
                {
                "https://www.googleapis.com/auth/firebase.messaging",
                "https://www.googleapis.com/auth/firebase.auth",
                "https://www.googleapis.com/auth/cloud-platform"
            }
            };

            var serviceAccountCredential = new ServiceAccountCredential(
                initializer.FromPrivateKey(serviceAccount.PrivateKey)
            );

            credential = GoogleCredential.FromServiceAccountCredential(serviceAccountCredential);
        }
        else
        {
            credential = GoogleCredential.GetApplicationDefault();
        }

        var appOptions = new AppOptions
        {
            Credential = credential,
            ProjectId = projectId
        };

        var firebaseApp = FirebaseApp.DefaultInstance;

        if (firebaseApp == null)
        {
            firebaseApp = FirebaseApp.Create(appOptions);
        }

        ValidateFirebaseEnvironment(firebaseApp, projectId, environment);

        services.AddSingleton(firebaseApp);
        services.AddSingleton(FirebaseAuth.GetAuth(firebaseApp));

        return services;
    }

    private static void ValidateFirebaseEnvironment(FirebaseApp app, string expectedProjectId, string environment)
    {
        if (string.IsNullOrWhiteSpace(expectedProjectId))
            throw new InvalidOperationException("Firebase:ProjectId não configurado.");

        if (app.Options.ProjectId != expectedProjectId)
        {
            throw new Exception(
                $"Projeto do Firebase incompatível. Esperava-se '{expectedProjectId}', mas foi recebido '{app.Options.ProjectId}'.");
        }

        if (environment == Environments.Production && !expectedProjectId.Contains("prod", StringComparison.OrdinalIgnoreCase))
        {
            throw new Exception("O ambiente de produção deve usar o projeto Firebase de produção.");
        }
    }
}

public class ServiceAccountKey
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = default!;

    [JsonPropertyName("project_id")]
    public string ProjectId { get; set; } = default!;

    [JsonPropertyName("private_key_id")]
    public string PrivateKeyId { get; set; } = default!;

    [JsonPropertyName("private_key")]
    public string PrivateKey { get; set; } = default!;

    [JsonPropertyName("client_email")]
    public string ClientEmail { get; set; } = default!;

    [JsonPropertyName("client_id")]
    public string ClientId { get; set; } = default!;
}
