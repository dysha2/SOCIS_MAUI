namespace SOCIS_MAUI.View;

public partial class AppLaunchPage : ContentPage
{
	public AppLaunchPage(AppLaunchVM VM)
	{
        this.BindingContext = VM;
        InitializeComponent();
    }
}