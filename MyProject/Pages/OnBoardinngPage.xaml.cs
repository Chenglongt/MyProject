namespace MyProject.Pages;

public partial class OnBoardinngPage : ContentPage
{
	public OnBoardinngPage()
	{
		InitializeComponent();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
		await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
    }

    private async void Signin_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(SigninPage));
    }

    private  async void Signup_Clicked(object sender, EventArgs e)//FlyoutFooter_�ⲿ��վ����
    {
        await Shell.Current.GoToAsync(nameof(SignupPage));
    }
}