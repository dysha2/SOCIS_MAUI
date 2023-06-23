namespace SOCIS_MAUI.View;

public partial class UnitPlacePage : ContentPage
{
	public UnitPlacePage(UnitPlaceVM VM)
	{
		BindingContext = VM;
		InitializeComponent();
	}
}