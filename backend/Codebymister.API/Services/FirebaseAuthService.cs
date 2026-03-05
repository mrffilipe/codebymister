using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;

namespace Codebymister.API.Services;

public class FirebaseAuthService
{
    private readonly FirebaseAuth _firebaseAuth;

    public FirebaseAuthService(IConfiguration configuration)
    {
        if (FirebaseApp.DefaultInstance == null)
        {
            var credentialPath = configuration["Firebase:CredentialsPath"];
            var credentialJson = Environment.GetEnvironmentVariable("FIREBASE_CREDENTIALS_JSON");
            
            if (!string.IsNullOrEmpty(credentialPath) && File.Exists(credentialPath))
            {
                FirebaseApp.Create(new AppOptions
                {
                    Credential = GoogleCredential.FromFile(credentialPath)
                });
            }
            else if (!string.IsNullOrEmpty(credentialJson))
            {
                FirebaseApp.Create(new AppOptions
                {
                    Credential = GoogleCredential.FromJson(credentialJson)
                });
            }
            else
            {
                throw new InvalidOperationException(
                    "Firebase credentials not configured. Please set either:\n" +
                    "1. Firebase:CredentialsPath in appsettings.json pointing to your service account JSON file, OR\n" +
                    "2. FIREBASE_CREDENTIALS_JSON environment variable with the JSON content"
                );
            }
        }

        _firebaseAuth = FirebaseAuth.DefaultInstance;
    }

    public async Task<FirebaseToken> VerifyTokenAsync(string token)
    {
        return await _firebaseAuth.VerifyIdTokenAsync(token);
    }

    public async Task<string> GetUserEmailAsync(string uid)
    {
        var user = await _firebaseAuth.GetUserAsync(uid);
        return user.Email;
    }
}
