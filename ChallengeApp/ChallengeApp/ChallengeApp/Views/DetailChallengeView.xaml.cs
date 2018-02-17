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
        ChallengeServices _challengeServices;

        Challenge _challenge;

        public DetailChallengeView(Challenge argChallenge)
        {
            InitializeComponent();

            // Guardo la info del Challenge aceptado
            _challenge = argChallenge;

            // Asgino el Challenge al Binding Context
            BindingContext = _challenge;

            // Creo el objeto HTTP para acceder a los servicios
            _challengeServices = new ChallengeServices();

            // No se si esta sea la mejor manera de mostrar el puntaje
            User userInfo = new User { UserPoints = "25" };

            LabelPoints.Text = String.Format("Points: {0}", userInfo.UserPoints);
        }

        async private void AcceptChallengeHandler(object sender, EventArgs e)
        {
            //await DisplayAlert("Challenge", "I Accept Your Challenge", "OK");
            var ServiceReponse = await _challengeServices.AcceptChallengeUser(_challenge);

            // TODO: Debo tratar la respuesta del servicio
            if (ServiceReponse == true)
            {
                await DisplayAlert("Challenge", "I Accept Your Challenge!", "OK");
            }
            else
            {
                await DisplayAlert("Error", "Ooops, Something is wrong, please try later.","OK");
            }

            // TODO: Actualizar la lista de retos aceptados
        

        }
    }
}