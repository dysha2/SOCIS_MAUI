namespace SOCIS_MAUI.View;

public partial class UnitPlaceEditPage : ContentPage
{
	public UnitPlaceEditPage(UnitPlaceEditVM VM)
	{
		BindingContext = VM;
		InitializeComponent();
	}
}