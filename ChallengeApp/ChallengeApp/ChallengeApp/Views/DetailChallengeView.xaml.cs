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

        //Expongo un event Handler para que la pagina principal pueda tomar los datos
        public event EventHandler<Challenge> ChallengeAdded;

        public DetailChallengeView(Challenge argChallenge, bool argExist)
        {
            InitializeComponent();

            // Guardo la info del Challenge aceptado
            _challenge = argChallenge;

            // Asgino el Challenge al Binding Context
            BindingContext = _challenge;

            // Creo el objeto HTTP para acceder a los servicios
            _challengeServices = new ChallengeServices();

            // Reviso si la bandera fue activada en la lista de UserChallenge
            btnAccept.IsVisible = argExist;
           
            // No se si esta sea la mejor manera de mostrar el puntaje
            User userInfo = new User { UserPoints = "25" };

            LabelPoints.Text = String.Format("Points: {0}", userInfo.UserPoints);
        }

        async private void AcceptChallengeHandler(object sender, EventArgs e)
        {
            //await DisplayAlert("Challenge", "I Accept Your Challenge", "OK");
            var ServiceReponse = await _challengeServices.AcceptChallengeUser(_challenge);

            // Pregunto por la respuesta del servicio
            if (ServiceReponse == true)
            {
                // Invoco el handler que se encuentra en la actividad vista ListChallengeView
                ChallengeAdded?.Invoke(this, _challenge);

                // Desactivo el boton para que el usuario no pueda activarlo de nuevo
                btnAccept.IsVisible = false;

                // Envio un mensaje de confirmacion
                await DisplayAlert("Challenge", "I Accept Your Challenge!", "OK");


            }
            else
            {
                await DisplayAlert("Error", "Ooops, Something is wrong, please try later.","OK");
            }

        }
    }
}