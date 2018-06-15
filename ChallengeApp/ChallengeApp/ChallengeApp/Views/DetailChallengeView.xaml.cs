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
    // Actividad que muestra el detalle del reto y tiene el boton de aceptacion

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailChallengeView : ContentPage
    {
        // Creo variables para guardar los servicios y los retos
        private ChallengeServices _challengeServices;
        private Challenge _challenge;

        //Expongo un event Handler para que la pagina ListChallengeView pueda tomar los datos
        public event EventHandler<Challenge> ChallengeAdded;

        //Expongo un event Handler para que la pagina ListChallengeView pueda tomar los datos
        public event EventHandler<Challenge> ChallengeQuitted;

        public DetailChallengeView(Challenge argChallenge, bool argExist) // argExist: me indica si esta pagina fue llamada en ListChallege o UserChallenge
        {
            InitializeComponent();

            // Guardo la info del Challenge seleccionado
            _challenge = argChallenge;

            // Asgino el Challenge al Binding Context
            BindingContext = _challenge;

            // Creo el objeto HTTP para acceder a los servicios
            _challengeServices = new ChallengeServices();

            // Reviso si la pagina fue llamada en ListChallege o UserChallenge
            btnAccept.IsVisible = argExist;
            btnQuit.IsVisible = !argExist;
           
            // No se si esta sea la mejor manera de mostrar el puntaje
            User userInfo = new User { UserPoints = "25" };

            LabelPoints.Text = String.Format("Points: {0}", userInfo.UserPoints);
        }

        // Metodo que se encarga de manejar el evento de aceptar el reto
        async private void AcceptChallengeHandler(object sender, EventArgs e)
        {
            // Invoco el servicio que acepta el reto en la DB
            var ServiceReponse = await _challengeServices.AcceptChallengeUser(_challenge);

            // Pregunto por la respuesta del servicio
            if (ServiceReponse == true)
            {
                // Invoco el handler que se encuentra en la actividad vista ListChallengeView
                ChallengeAdded?.Invoke(this, _challenge);

                // Desactivo el boton para que el usuario no pueda activarlo de nuevo
                btnAccept.IsVisible = false;
                btnQuit.IsVisible = true;

                // Envio un mensaje de confirmacion
                await DisplayAlert("Challenge", "I Accept Your Challenge!", "OK");

            }
            else
            {
                await DisplayAlert("Error", "Ooops, Something is wrong, please try later.","OK");
            }

            await Navigation.PopAsync();

        }

        // Metodo que se encarga del metodo de renunciar al reto
        async private void QuitChallengeHandler(object sender, EventArgs e)
        {
            // Invoco el servicio que renuncia el reto en la base de datos
            var ServiceReponse = await _challengeServices.QuitChallengeUser(_challenge);

            // Pregunto por la respuesta del servicio
            if (ServiceReponse == true)
            {
                // Invoco el handler que se encuentra en la actividad vista ListChallengeView
                ChallengeQuitted?.Invoke(this, _challenge);

                // Desactivo el boton para que el usuario no pueda activarlo de nuevo
                btnAccept.IsVisible = true;
                btnQuit.IsVisible = false;

                // Envio un mensaje de confirmacion
                await DisplayAlert("Challenge", "Are you too scare??", "Yes I'm");

            }
            else
            {
                await DisplayAlert("Error", "Ooops, Something is wrong, please try later.", "OK");
            }

            await Navigation.PopAsync();
        }
    }
}