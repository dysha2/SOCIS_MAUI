namespace SOCIS_MAUI.View;

public partial class FullNameUnitPage : ContentPage
{
	public FullNameUnitPage(FullNameUnitVM VM)
	{
		BindingContext = VM;
		InitializeComponent();
	}
}