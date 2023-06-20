namespace SOCIS_MAUI.View;

public partial class AccountingUnitPage : ContentPage
{
	public AccountingUnitPage(AccountingUnitVM VM)
	{
		BindingContext = VM;
		InitializeComponent();
	}
}