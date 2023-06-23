namespace SOCIS_MAUI.View;

public partial class UnitRespPersonPage : ContentPage
{
	public UnitRespPersonPage(UnitRespPersonVM VM)
	{
		BindingContext = VM;
		InitializeComponent();
	}
}