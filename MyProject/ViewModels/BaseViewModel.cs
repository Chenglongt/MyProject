using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool _isBusy;

        protected async Task GoToAsync(string url, bool animate = false)=> await Shell.Current.GoToAsync(url, animate: true); 
        protected async Task DisplayErrorAlertAsync(string errorInformation)=> await Shell.Current.DisplayAlert("Error",errorInformation , "Ok");
        protected async Task DisplayAlertAsync(string information)=> await Shell.Current.DisplayAlert("Alert",information , "Ok");
    }
}
