namespace SOCIS_MAUI.View;

public partial class FirmPage : ContentPage
{
	public FirmPage(FirmVM VM)
	{
		BindingContext = VM;
		InitializeComponent();
	}
}