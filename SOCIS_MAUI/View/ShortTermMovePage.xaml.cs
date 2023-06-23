namespace SOCIS_MAUI.View;

public partial class ShortTermMovePage : ContentPage
{
	public ShortTermMovePage(ShortTermMoveVM VM)
	{
		BindingContext = VM;
		InitializeComponent();
	}
}