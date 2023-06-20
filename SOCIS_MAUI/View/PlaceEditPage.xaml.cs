namespace SOCIS_MAUI.View;

public partial class PlaceEditPage : ContentPage
{
	public PlaceEditPage(PlaceEditVM VM)
	{
        BindingContext = VM;
        InitializeComponent();
    }
}