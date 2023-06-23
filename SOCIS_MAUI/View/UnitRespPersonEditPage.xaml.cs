namespace SOCIS_MAUI.View;

public partial class UnitRespPersonEditPage : ContentPage
{
	public UnitRespPersonEditPage(UnitRespPersonEditVM VM)
	{
		BindingContext = VM;
		InitializeComponent();
	}
}