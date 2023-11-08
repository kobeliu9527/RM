using Microsoft.Extensions.Logging;
using MobileClient.Data;
using RM.Shared;
namespace MobileClient
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddBootstrapBlazor();
            builder.Services.AddHttpClient(name: "HttpClient", c =>
            {
                //https://kjkqffn5-5000.asse.devtunnels.ms/
               // c.BaseAddress = new Uri("http://192.168.125.13:5000");
                c.BaseAddress = new Uri("https://kjkqffn5-5000.asse.devtunnels.ms/");
            }
);
#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton<WeatherForecastService>();

            return builder.Build();
        }
    }
}