namespace SOCIS_MAUI.View;

public partial class ShortTermMoveEditPage : ContentPage
{
	public ShortTermMoveEditPage(ShortTermMoveEditVM VM)
	{
		BindingContext = VM;
		InitializeComponent();
	}
}