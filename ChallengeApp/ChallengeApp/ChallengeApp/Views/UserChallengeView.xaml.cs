using ChallengeApp.Models;
using ChallengeApp.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private List<Challenge> _listChallenge { get; set; }
        private ObservableCollection<Challenge> _obsListChallenge { get; set; }

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
            if (_obsListChallenge != null)
            {
                // Reviso si debo añadir otro challenge a la lista
                if (Application.Current.Properties.ContainsKey(Constans.UserChallenge))
                {
                    string txtChallenge = Application.Current.Properties[Constans.UserChallenge].ToString();
                    _obsListChallenge.Add(JsonConvert.DeserializeObject<Challenge>(txtChallenge));

                    // Borro el contenido de la variable
                    Application.Current.Properties.Remove(Constans.UserChallenge);
                }

                return;
            };

            var userServices = new UserServices();

            // Creo una lista que me guarde los Challenge
            _listChallenge = await userServices.GetUserChallengeList();

            // Creo una lista que me guarde los Challenge seleccionados por el usuario
            _obsListChallenge = new ObservableCollection<Challenge>(_listChallenge);

            ListChallenge.ItemsSource = _obsListChallenge;
        }

        async private void ListChallenge_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            // Eso lo debo de hacer para que no me levante otra vista cuando haga la deseleccion
            if (ListChallenge.SelectedItem == null)
                return;

            // Deselecciono el elemento de la lista
            ListChallenge.SelectedItem = null;

            var selChallenge = e.SelectedItem as Challenge;

            await Navigation.PushAsync(new DetailChallengeView(selChallenge, false)); //-> False: quiero que el boton no se muestre
        }
    }
}