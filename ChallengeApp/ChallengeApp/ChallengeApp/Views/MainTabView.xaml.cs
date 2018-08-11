using ChallengeApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChallengeApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainTabView : TabbedPage
    {
       
        public MainTabView()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();       
        }

        private async void ToolbarItem_Activated(object sender, EventArgs e)
        {
            var response = await DisplayAlert("Exit", "Are you sure?", "Yes", "No");
            if (response)
            {
                // Invoco el servicio de Logout
                UserServices _userServices = new UserServices();
                _userServices.UserLogout();

                //LLamo el servicio que se encarga del Logout y vuelvo a la pagina de Login
                await Navigation.PopAsync();
            }
                     
        }
    }
}