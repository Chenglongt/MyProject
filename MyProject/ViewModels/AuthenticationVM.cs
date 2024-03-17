using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyProject.Pages;
using MyProject.Services;
using MyProject.Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyProject.ViewModels
{
    public partial class AuthenticationVM(IAuthApi authApi) : BaseViewModel
    {
        private readonly IAuthApi _authApi= authApi;

        [ObservableProperty, NotifyPropertyChangedFor(nameof(CanSignup))]
        private string? _userName;

        [ObservableProperty, NotifyPropertyChangedFor(nameof(CanSignin)), NotifyPropertyChangedFor(nameof(CanSignup))]
        private string? _password;

        [ObservableProperty, NotifyPropertyChangedFor(nameof(CanSignin)), NotifyPropertyChangedFor(nameof(CanSignup))]
        private string? _email;

        [ObservableProperty, NotifyPropertyChangedFor(nameof(CanSignup))]
        private string? _address;

        public bool CanSignup => !string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Address);

        public bool CanSignin => !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password);

        [RelayCommand]
        private async Task SignupAsync()
        {
            IsBusy = true;
            try { var signupDataTransferObjects = new SginupRequestDataTransferObjects(UserName, Password, Email, Address);
                var result = await _authApi.SignupAsync(signupDataTransferObjects);
                if (result.SuccessLoggin) {
                    await DisplayAlertAsync(result.Data.Token);

                    await GoToAsync($"//{nameof(HomePage)}", animate: true); }
                else { await DisplayErrorAlertAsync(result.ErrorInformation ?? " error in signing up"); }
            }

            catch (Exception ex) { await DisplayErrorAlertAsync(ex.Message); }
            finally { IsBusy = false; }
        }

        [RelayCommand]
        private async Task SigninAsync() {
            IsBusy = true;
            try
            {
                var signinDataTransferObjects = new SgininRequestDataTransferObjects(Email,Password);
                var result = await _authApi.SigninAsync(signinDataTransferObjects);
                if (result.SuccessLoggin) {

                    await DisplayAlertAsync(result.Data.User.Nmae);

                    await GoToAsync($"//{nameof(HomePage)}", animate: true); }
                else { await DisplayErrorAlertAsync(result.ErrorInformation ?? " error in signing in"); }
            }

            catch (Exception ex) { await DisplayErrorAlertAsync(ex.Message); }
            finally { IsBusy = false; }
        }

    }
}
