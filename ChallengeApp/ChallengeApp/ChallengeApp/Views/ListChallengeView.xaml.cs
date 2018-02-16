using ChallengeApp.Models;
using ChallengeApp.Services;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChallengeApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListChallengeView : ContentPage
    {
        private List<Challenge> listChallenge { get; set; }

        public ListChallengeView()
        {
            InitializeComponent();

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

            // Creo la clas eque llama el servicio
            var challengeServices = new ChallengeServices();

            // Creo una lista que me guarde los Challenge
            listChallenge = new List<Challenge>();

            listChallenge = await challengeServices.GetAllChallenges();

            ListChallenge.ItemsSource = listChallenge;
        }

        async private void ListChallenge_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            // Eso lo debo de hacer para que no me levante otra vista cuando haga la deseleccion
            if (ListChallenge.SelectedItem == null)
                return;

            // Deselecciono el elemento de la lista
            ListChallenge.SelectedItem = null;

            // Capturo el elemento seleccionado
            var challenge = e.SelectedItem as Challenge;

            // Llamo a la pagina de detalles
            await Navigation.PushAsync(new DetailChallengeView(challenge));

            
        }
    }
}