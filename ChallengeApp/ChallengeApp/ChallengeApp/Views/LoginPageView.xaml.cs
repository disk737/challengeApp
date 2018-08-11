using ChallengeApp.Models;
using ChallengeApp.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        private string oldToken;

        private bool activityActivated = false;

        public LoginPageView()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            if (activityActivated)
                return;

            //Activo la bandera para que no se vuelva a llamar el Psuh de MainTabView
            activityActivated = true;

            // Reviso si tengo un Token viejo que pueda usar. Esto es un seguro
            if (Application.Current.Properties.ContainsKey(Constans.UserTokenString))
            {
                // Extraigo el viejo Token
                oldToken = Application.Current.Properties[Constans.UserTokenString].ToString();
            }
            
            if (oldToken != null && 
                Application.Current.Properties.ContainsKey(Constans.SaveCredentials) && 
                Application.Current.Properties[Constans.SaveCredentials] as string == Constans.SaveActive)
            {
                // Llamo la pagina principal de Tabs
                await Navigation.PushAsync(new MainTabView());
            }

            _userServices = new UserServices();
        }

        async private void LoginHandler(object sender, EventArgs e)
        {

            // Valido las credenciales ingresadas por el usuario
            var userToken = await _userServices.UserSignIn(EntryEmail.Text, EntryPassword.Text);
                       
            // Reviso si obtengo un Token o un mensaje de error
            if (userToken.Token != null)
            {
                // Guardo el token generado para el usuario
                Application.Current.Properties[Constans.UserTokenString] = userToken.Token;

                // Si el usuario decidio guardar sus credenciales, procedo a guardarlas
                if (SaveCredencials.IsToggled == true)
                    Application.Current.Properties[Constans.SaveCredentials] = Constans.SaveActive;
                else
                    Application.Current.Properties[Constans.SaveCredentials] = Constans.SaveUnactive;

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