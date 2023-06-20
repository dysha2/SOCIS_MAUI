namespace SOCIS_MAUI.View;

public partial class AccountingUnitEditPage : ContentPage
{
	public AccountingUnitEditPage(AccountingUnitEditVM VM)
	{
		BindingContext = VM;
		InitializeComponent();
	}
}