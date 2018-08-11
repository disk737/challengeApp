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

        public UserChallengesView ()
		{
			InitializeComponent ();

            //// No se si esta sea la mejor manera de mostrar el puntaje
            //User userInfo = new User { UserPoints = "25" };

            //LabelPoints.Text = String.Format("Points: {0}", userInfo.UserPoints);

        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            // Reviso si la lista fue cargada en un momento anterior
            if (SingletonChallenge.Instance._obsUserListChallenge != null)
                return;

            var userServices = new ChallengeServices();

            // Creo una lista que me guarde los Challenge
            _listChallenge = await userServices.GetUserChallengeList();

            // Creo una lista que me guarde los Challenge seleccionados por el usuario
            SingletonChallenge.Instance._obsUserListChallenge = new ObservableCollection<Challenge>(_listChallenge);

            ListChallenge.ItemsSource = SingletonChallenge.Instance._obsUserListChallenge;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            // Borro el contenido de los array temporales
            //_quittedListChallenge.Clear();
        }

        async private void UserChallenge_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            // Eso lo debo de hacer para que no me levante otra vista cuando haga la deseleccion
            if (ListChallenge.SelectedItem == null)
                return;

            // Deselecciono el elemento de la lista
            ListChallenge.SelectedItem = null;

            var selChallenge = e.SelectedItem as Challenge;

            // Creo la pagina de detalles que va ser abierta
            var detailPage = new DetailChallengeView(selChallenge, false); //-> False: Indico que estoy llamando desde UserChallengeList

            // Agrego el Handler creado a la pagina de detalles
            detailPage.ChallengeQuitted += (source, challenge) =>
            {
                // Remuevo el Challenge de esta lista de aceptados
                SingletonChallenge.Instance._obsUserListChallenge.Remove(challenge);

                // Agrego el Challenge a la lista de Rechazados
                if (SingletonChallenge.Instance._obsListChallenge != null)
                {
                    SingletonChallenge.Instance._obsListChallenge.Add(challenge);
                }
            };

            await Navigation.PushAsync(detailPage); 
        }
    }
}