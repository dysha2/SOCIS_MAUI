namespace SOCIS_MAUI.View;

public partial class FirmEditPage : ContentPage
{
	public FirmEditPage(FirmEditVM VM)
	{
		BindingContext = VM;
		InitializeComponent();
	}
}