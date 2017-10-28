using ChallengeApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChallengeApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UserChallengesView : ContentPage
	{
		public UserChallengesView ()
		{
			InitializeComponent ();

            // Esto debe ser un servicio
            ListChallenge.ItemsSource = new List<Challenge>
            {
                new Challenge{ChallengeName = "Explore", DescripChallenge = "Debes ir a tal lado y tomar una fotografia...", EvidenChallenge = "Foto por Email", ChallengePoint ="5"},
                new Challenge{ChallengeName ="Read", DescripChallenge = "Debes leer el capitulo y enviar un resumen...", EvidenChallenge = "Enviar resumen por Email", ChallengePoint = "3"},
                new Challenge{ChallengeName = "Demostrate", DescripChallenge = "Realiza el experimiento 13 y enviar el resultado...", EvidenChallenge = "Foto por Email", ChallengePoint = "8"}
            };

            // No se si esta sea la mejor manera de mostrar el puntaje
            UserInfo userInfo = new UserInfo { UserPoints = "25" };

            LabelPoints.Text = String.Format("Points: {0}", userInfo.UserPoints);

        }

        async private void ListChallenge_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

            var challenge = e.SelectedItem as Challenge;

            await Navigation.PushAsync(new DetailChallengeView(challenge));
        }
    }
}