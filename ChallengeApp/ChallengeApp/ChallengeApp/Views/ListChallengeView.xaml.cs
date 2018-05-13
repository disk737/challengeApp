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
        private ObservableCollection<Challenge> _obsListChallenge { get; set; }

        private List<Challenge> _acceptedListChallenge { get; set; }

        public ListChallengeView()
        {
            InitializeComponent();

            // Inicializo la lista de Retos Aceptados
            _acceptedListChallenge = new List<Challenge>();

            // No se si esta sea la mejor manera de mostrar el puntaje
            User userInfo = new User { UserPoints = "25" };

            LabelPoints.Text = String.Format("Points: {0}", userInfo.UserPoints);
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            // Borro el contenido del array auxiliar si hubo un cambio en la Actividad
            if (Application.Current.Properties.ContainsKey(Constans.FlagChallengeList))
            {
                // Reviso si la bandera esta en True
                if(Application.Current.Properties[Constans.FlagChallengeList] as string == "true")
                {
                    // Borro el contenido del array y bajo la bandera
                    _acceptedListChallenge.Clear();
                    Application.Current.Properties[Constans.FlagChallengeList] = "false";
                }
                
            }

            // Reviso si la lista fue cargada en un momento anterior
            if (_obsListChallenge != null)
            {
                // Reviso si debo añadir otro challenge a la lista
                if (Application.Current.Properties.ContainsKey(Constans.QuitChallengeUser))
                {
                    string txtChallenge = Application.Current.Properties[Constans.QuitChallengeUser].ToString();

                    // Deserializo el array de Retos Rechazados por el usuario
                    List<Challenge> AuxChallengeList = JsonConvert.DeserializeObject<List<Challenge>>(txtChallenge);

                    // Ingreso todos los Retos añadidos la actividad ListChallengeView
                    foreach (var item in AuxChallengeList)
                    {
                        _obsListChallenge.Add(item);
                    }

                    // Borro el contenido de la variable
                    Application.Current.Properties.Remove(Constans.QuitChallengeUser);

                    // Cambio la bandera indicando que ya se actualizaron los Rechazos
                    Application.Current.Properties[Constans.FlagUserList] = "true";

                }

                return;
            }
                
            // Creo la clase que llama el servicio
            var challengeServices = new ChallengeServices();

            // Obtengo la lista de retos desde el servicio
            _listChallenge = await challengeServices.GetAllChallenges();

            // Creo una lista que me guarde los Challenge
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

            // Capturo el elemento seleccionado
            var selChallenge = e.SelectedItem as Challenge;

            // Creo la pagina de Detalles y agrego contenido al Handler para tener la referencia de la lista
            var detailPage = new DetailChallengeView(selChallenge, true); //-> True: Para que se muestre siempre el boton de "Aceptar Reto"

            // Agrego el Handler a la pagina creada
            detailPage.ChallengeAdded += (source, challenge) =>
            {
                // Remuevo el reto a la lista de aceptados
                _obsListChallenge.Remove(challenge);

                // Agreo el reto en la lista
                _acceptedListChallenge.Add(challenge);

                // Guardo el array de Challenge para que pueda ser pasado a la otra lista
                Application.Current.Properties[Constans.AcceptChallengeUser] = JsonConvert.SerializeObject(_acceptedListChallenge, Formatting.Indented);

            };

            // Llamo a la pagina de detalles
            await Navigation.PushAsync(detailPage);

        }
    }
}