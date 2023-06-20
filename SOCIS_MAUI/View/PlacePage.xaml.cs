namespace SOCIS_MAUI.View;

public partial class PlacePage : ContentPage
{
	public PlacePage(PlaceVM placeVM)
	{
		BindingContext = placeVM;
		InitializeComponent();
	}
}