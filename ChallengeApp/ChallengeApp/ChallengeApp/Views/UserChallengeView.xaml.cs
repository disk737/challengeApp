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
        private ObservableCollection<Challenge> _obsUserListChallenge { get; set; }

        private List<Challenge> _quittedListChallenge { get; set; }

        public UserChallengesView ()
		{
			InitializeComponent ();

            // Inicializo la lista de Retos Rechazados
            _quittedListChallenge = new List<Challenge>();

            // No se si esta sea la mejor manera de mostrar el puntaje
            User userInfo = new User { UserPoints = "25" };

            LabelPoints.Text = String.Format("Points: {0}", userInfo.UserPoints);

        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            // Borro el contenido del array auxiliar si hubo un cambio en la Actividad
            if (Application.Current.Properties.ContainsKey(Constans.FlagUserList))
            {

                // Reviso si la bandera esta en True
                if ((Application.Current.Properties[Constans.FlagUserList].ToString() == "true"))
                {
                    // Borro el contenido del array y bajo la bandera
                    _quittedListChallenge.Clear();
                    Application.Current.Properties[Constans.FlagUserList] = "false";
                }

            }

            // Reviso si la lista fue cargada en un momento anterior
            if (_obsUserListChallenge != null)
            {
                // Reviso si debo añadir otro challenge a la lista
                if (Application.Current.Properties.ContainsKey(Constans.AcceptChallengeUser))
                {
                    string txtChallenge = Application.Current.Properties[Constans.AcceptChallengeUser].ToString();

                    // Deserializo el array de Retos aceptados por el usuario
                    List<Challenge> AuxChallengeList = JsonConvert.DeserializeObject<List<Challenge>>(txtChallenge);

                    // Ingreso todos los Retos añadidos la actividad ListChallengeView
                    foreach (var item in AuxChallengeList)
                    {
                        _obsUserListChallenge.Add(item);
                    }
                   
                    // Borro el contenido de la variable
                    Application.Current.Properties.Remove(Constans.AcceptChallengeUser);

                    // Cambio la bandera indicando que ya se actualizaron las Aceptaciones
                    Application.Current.Properties[Constans.FlagChallengeList] = "true";
                }

                return;
            };

            var userServices = new UserServices();

            // Creo una lista que me guarde los Challenge
            _listChallenge = await userServices.GetUserChallengeList();

            // Creo una lista que me guarde los Challenge seleccionados por el usuario
            _obsUserListChallenge = new ObservableCollection<Challenge>(_listChallenge);

            ListChallenge.ItemsSource = _obsUserListChallenge;
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
                // Remuevo el Challenge de esta lista
                _obsUserListChallenge.Remove(challenge);

                // Agrego el Challenge a la lista de Rechazados
                _quittedListChallenge.Add(challenge);

                // Guardo el arreglo de Challenge para que pueda ser pasado a la otra lista
                Application.Current.Properties[Constans.QuitChallengeUser] = JsonConvert.SerializeObject(_quittedListChallenge, Formatting.Indented);

            };

            await Navigation.PushAsync(detailPage); 
        }
    }
}