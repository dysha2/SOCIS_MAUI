global using SOCIS_MAUI.Services;
global using SOCIS_MAUI.ViewModel;
global using SOCIS_MAUI.View;
global using SOCIS_MAUI_MODEL.Model;
global using SOCIS_MAUI_MODEL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SOCIS_MAUI.View;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using CommunityToolkit.Maui;

namespace SOCIS_MAUI;


public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});
        var a = Assembly.GetExecutingAssembly();
        using var stream = a.GetManifestResourceStream("SOCIS_MAUI.appsettings.json");
        var config = new ConfigurationBuilder()
            .AddJsonStream(stream)
            .Build();
        builder.Configuration.AddConfiguration(config);

		builder.Services.AddSingleton<DisplayService>();
        builder.Services.AddSingleton<INavigationService,NavigationService>();
		builder.Services.AddSingleton<DataTransferService>();
		builder.Services.AddSingleton<MainModel>();
		//UI Reg
		builder.Services.AddSingleton<AppLaunchPage>();
		builder.Services.AddSingleton<PlacePage>();
		builder.Services.AddTransient<PlaceEditPage>();
		builder.Services.AddSingleton<FirmPage>();
		builder.Services.AddTransient<FirmEditPage>();
        builder.Services.AddSingleton<UnitTypePage>();
        builder.Services.AddTransient<UnitTypeEditPage>();
        builder.Services.AddSingleton<FullNameUnitPage>();
        builder.Services.AddTransient<FullNameUnitEditPage>();
        builder.Services.AddSingleton<AccountingUnitPage>();
        builder.Services.AddTransient<AccountingUnitEditPage>();
        builder.Services.AddTransient<UnitPlacePage>();
        builder.Services.AddTransient<UnitPlaceEditPage>();
        builder.Services.AddTransient<ShortTermMovePage>();
        builder.Services.AddTransient<ShortTermMoveEditPage>();
        builder.Services.AddTransient<UnitRespPersonPage>();
        builder.Services.AddTransient<UnitRespPersonEditPage>();
        //VM Reg
        builder.Services.AddSingleton<AppLaunchVM>();
		builder.Services.AddSingleton<PlaceVM>();
		builder.Services.AddTransient<PlaceEditVM>();
		builder.Services.AddSingleton<FirmVM>();
		builder.Services.AddTransient<FirmEditVM>();
        builder.Services.AddSingleton<UnitTypeVM>();
        builder.Services.AddTransient<UnitTypeEditVM>();
        builder.Services.AddSingleton<FullNameUnitVM>();
        builder.Services.AddTransient<FullNameUnitEditVM>();
        builder.Services.AddSingleton<AccountingUnitVM>();
        builder.Services.AddTransient<AccountingUnitEditVM>();
        builder.Services.AddTransient<UnitPlaceVM>();
        builder.Services.AddTransient<UnitPlaceEditVM>();
        builder.Services.AddTransient<ShortTermMoveVM>();
        builder.Services.AddTransient<ShortTermMoveEditVM>();
        builder.Services.AddTransient<UnitRespPersonVM>();
        builder.Services.AddTransient<UnitRespPersonEditVM>();
        return builder.Build();
	}
}
