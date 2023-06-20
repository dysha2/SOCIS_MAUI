namespace SOCIS_MAUI.View;

public partial class UnitTypeEditPage : ContentPage
{
	public UnitTypeEditPage(UnitTypeEditVM VM)
	{
		BindingContext = VM;
		InitializeComponent();
	}
}