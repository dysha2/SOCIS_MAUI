namespace SOCIS_MAUI.View;

public partial class FullNameUnitEditPage : ContentPage
{
	public FullNameUnitEditPage(FullNameUnitEditVM VM)
	{
		BindingContext = VM;
		InitializeComponent();
	}
}