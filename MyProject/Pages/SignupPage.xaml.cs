using MyProject.ViewModels;

namespace MyProject.Pages;

public partial class SignupPage : ContentPage
{
	public SignupPage(AuthenticationVM authenticationVM)
	{
		InitializeComponent();
		BindingContext = authenticationVM;
	}

    private async void Signin_Tapped(object sender, TappedEventArgs e)
    {
		await Shell.Current.GoToAsync(nameof(SigninPage));
    }
}