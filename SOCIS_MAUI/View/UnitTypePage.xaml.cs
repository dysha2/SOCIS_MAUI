namespace SOCIS_MAUI.View;

public partial class UnitTypePage : ContentPage
{
	public UnitTypePage(UnitTypeVM VM)
	{
		BindingContext = VM;
		InitializeComponent();
	}
}