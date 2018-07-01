using ChallengeApp.Models;
using ChallengeApp.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChallengeApp.Views

    // Catividad que contiene todas los retos que el usuario no ha aceptado
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListChallengeView : ContentPage
    {
        private List<Challenge> _listChallenge { get; set; }

        public ListChallengeView()
        {
            InitializeComponent();

            //// No se si esta sea la mejor manera de mostrar el puntaje
            //User userInfo = new User { UserPoints = "25" };

            //LabelPoints.Text = String.Format("Points: {0}", userInfo.UserPoints);
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            // Reviso si la lista fue cargada en un momento anterior
            if (SingletonChallenge.Instance._obsListChallenge != null)
                return;
                
            // Creo la clase que llama el servicio
            var challengeServices = new ChallengeServices();

            // Obtengo la lista de retos desde el servicio
            _listChallenge = await challengeServices.GetAllChallenges();

            // Creo una lista que me guarde los Challenge
            //_obsListChallenge = new ObservableCollection<Challenge>(_listChallenge);
            SingletonChallenge.Instance._obsListChallenge = new ObservableCollection<Challenge>(_listChallenge);

            //ListChallenge.ItemsSource = _obsListChallenge;
            ListChallenge.ItemsSource = SingletonChallenge.Instance._obsListChallenge;
        }

        async private void ListChallenge_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            // Eso lo debo de hacer para que no me levante otra vista cuando haga la deseleccion
            if (ListChallenge.SelectedItem == null)
                return;

            // Deselecciono el elemento de la lista
            ListChallenge.SelectedItem = null;

            // Capturo el elemento seleccionado
            var selChallenge = e.SelectedItem as Challenge;

            // Creo la pagina de Detalles y agrego contenido al Handler para tener la referencia de la lista
            var detailPage = new DetailChallengeView(selChallenge, true); //-> True: Para que se muestre siempre el boton de "Aceptar Reto"

            // Agrego el Handler a la pagina creada
            detailPage.ChallengeAdded += (source, challenge) =>
            {
                // Remuevo el reto a la lista de aceptados
                SingletonChallenge.Instance._obsListChallenge.Remove(challenge);

                // Agreo el reto en la lista de retos aceptados
                if (SingletonChallenge.Instance._obsUserListChallenge != null)
                {
                    SingletonChallenge.Instance._obsUserListChallenge.Add(challenge);
                }
                
            };

            // Llamo a la pagina de detalles
            await Navigation.PushAsync(detailPage);

        }
    }
}