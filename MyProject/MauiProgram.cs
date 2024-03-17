using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using Refit;
using MyProject.Services;
using MyProject.ViewModels;
using MyProject.Pages;



#if ANDROID
using Xamarin.Android.Net;
using System.Net.Security;
#elif IOS
using Security;
#endif

namespace MyProject
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>().ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            }).UseMauiCommunityToolkit();
#if DEBUG
            builder.Logging.AddDebug();
#endif
            builder.Services.AddTransient<AuthenticationVM>().AddTransient<SignupPage>().AddTransient<SigninPage>();

            ConfigureRefit(builder.Services);
            return builder.Build();
        }

        private static void ConfigureRefit(IServiceCollection services) {
            var refitSettings = new RefitSettings {
                HttpMessageHandlerFactory =() => {
#if ANDROID
                    return new AndroidMessageHandler
                    {
                        ServerCertificateCustomValidationCallback = (httpRequestMessage, certificate, chain, sslPolicyErrors) =>
                        {
                            return certificate?.Issuer == "CN=localhost" || sslPolicyErrors == SslPolicyErrors.None;
                        }
                    }; 
#elif IOS
                    return new NSUrlSessionHandler
                    {
                        TrustOverrideForUrl = (NSUrlSessionHandler sender, string url, SecTrust trust) =>
                        url.StartsWith("https://localhost")
                    };
#endif
                    return null;
                }
            };
            services.AddRefitClient<IAuthApi>(refitSettings)
                .ConfigureHttpClient(httpClient => {
                    var baseUrl = DeviceInfo.Platform == DevicePlatform.Android ? "https://10.0.2.2:7015" : "https://localhost:7015";
                    httpClient.BaseAddress = new Uri(baseUrl); });
        }
    }
}