using ChallengeApp.Models;
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
    // Actividad que contiene la lista de los retos aceptados por el usuario

	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UserChallengesView : ContentPage
	{
        private UserServices _userServices;

        private List<Challenge> listChallenge { get; set; }

        public UserChallengesView ()
		{
			InitializeComponent ();

            // No se si esta sea la mejor manera de mostrar el puntaje
            User userInfo = new User { UserPoints = "25" };

            LabelPoints.Text = String.Format("Points: {0}", userInfo.UserPoints);

        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            // Reviso si la lista fue cargada en un momento anterior
            if (listChallenge != null)
                return;

            _userServices = new UserServices();

            // Creo una lista que me guarde los Challenge
            listChallenge = new List<Challenge>();

            listChallenge = await _userServices.GetUserChallengeList();

            ListChallenge.ItemsSource = listChallenge;
        }

        async private void ListChallenge_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            // Eso lo debo de hacer para que no me levante otra vista cuando haga la deseleccion
            if (ListChallenge.SelectedItem == null)
                return;

            // Deselecciono el elemento de la lista
            ListChallenge.SelectedItem = null;

            var challenge = e.SelectedItem as Challenge;

            await Navigation.PushAsync(new DetailChallengeView(challenge));
        }
    }
}