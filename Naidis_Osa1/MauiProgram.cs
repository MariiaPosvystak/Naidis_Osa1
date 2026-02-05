using Microsoft.Extensions.Logging;

namespace Naidis_Osa1
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
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Lower-WestSide 400.ttf", "LowerWestSide400");
                });

#if DEBUG
    		builder.Logging.AddDebug(); 
#endif

            return builder.Build();
        }
    }
}
