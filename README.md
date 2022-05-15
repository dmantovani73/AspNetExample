# Applicazione ASP.NET 6 di esempio
- CRUD "tradizionale"
- CRUS AJAX via JsGrid
- Autenticazione

# Setup
- Creare un'ApiKey SendGrid: https://www.twilio.com/blog/send-emails-using-the-sendgrid-api-with-dotnetnet-6-and-csharp
- Creare un'app Facebook: https://docs.microsoft.com/it-it/aspnet/core/security/authentication/social/facebook-logins?view=aspnetcore-6.0
- Fare il setup dei seguenti secret per il progetto Example.WebApp:
```json
{
  "SendGrid": {
    "ApiKey": "{SendGrid ApiKey}",
    "From": "{SendGrid Sender}"
  },

  "Authentication:Facebook:AppId": "{Facebook AppId}",
  "Authentication:Facebook:AppSecret": "{Facebook AppSecret}"
}
```
