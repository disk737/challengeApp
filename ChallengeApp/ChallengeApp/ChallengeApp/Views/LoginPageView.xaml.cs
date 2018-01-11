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
    public partial class LoginPageView : ContentPage
    {
        private UserServices _userServices;

        public LoginPageView()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            _userServices = new UserServices();
        }

        async private void LoginHandler(object sender, EventArgs e)
        {

            var userToken = await _userServices.UserSignIn(EntryEmail.Text,EntryPassword.Text);
           
            // Reviso si obtengo un Token o un mensaje de error
            if (userToken.Token != null)
            {
                // Operacion Exitosa
                //await DisplayAlert("Challenge", userToken.Token, "OK");

                // Llamo la pagina principal de Tabs
                await Navigation.PushAsync(new MainTabView());
            }
            else
            {
                // Operacion Fallida
                await DisplayAlert("Challenge", userToken.message, "OK");
            }
   
        }
    }
}