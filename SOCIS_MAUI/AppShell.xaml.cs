namespace SOCIS_MAUI;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute(nameof(PlacePage),typeof(PlacePage));
        Routing.RegisterRoute(nameof(PlaceEditPage), typeof(PlaceEditPage));
        Routing.RegisterRoute(nameof(FirmPage), typeof(FirmPage));
        Routing.RegisterRoute(nameof(FirmEditPage), typeof(FirmEditPage));
        Routing.RegisterRoute(nameof(UnitTypePage), typeof(UnitTypePage));
        Routing.RegisterRoute(nameof(UnitTypeEditPage), typeof(UnitTypeEditPage));
        Routing.RegisterRoute(nameof(FullNameUnitPage), typeof(FullNameUnitPage));
        Routing.RegisterRoute(nameof(FullNameUnitEditPage), typeof(FullNameUnitEditPage));
        Routing.RegisterRoute(nameof(AccountingUnitPage), typeof(AccountingUnitPage));
        Routing.RegisterRoute(nameof(AccountingUnitEditPage), typeof(AccountingUnitEditPage));
        Routing.RegisterRoute(nameof(UnitPlacePage), typeof(UnitPlacePage));
        Routing.RegisterRoute(nameof(UnitPlaceEditPage), typeof(UnitPlaceEditPage));
        Routing.RegisterRoute(nameof(ShortTermMovePage), typeof(ShortTermMovePage));
        Routing.RegisterRoute(nameof(ShortTermMoveEditPage), typeof(ShortTermMoveEditPage));
        Routing.RegisterRoute(nameof(UnitRespPersonPage), typeof(UnitRespPersonPage));
        Routing.RegisterRoute(nameof(UnitRespPersonEditPage), typeof(UnitRespPersonEditPage));
    }
}
