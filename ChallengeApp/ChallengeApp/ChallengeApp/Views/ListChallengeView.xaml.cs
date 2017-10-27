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
    public partial class ListChallengeView : ContentPage
    {
        public ListChallengeView()
        {
            InitializeComponent();

            // Esto debe ser un servicio
            ListChallenge.ItemsSource = new List<Challenge>
            {
                new Challenge{ChallengeName = "Explore", ChallengePoint ="5"},
                new Challenge{ChallengeName ="Read", ChallengePoint = "3"},
                new Challenge{ChallengeName = "Demostrate", ChallengePoint = "8"}
            };

            // No se si esta sea la mejor manera de mostrar el puntaje
            UserInfo userInfo = new UserInfo { UserPoints = "25" };

            LabelPoints.Text = String.Format("Points: {0}", userInfo.UserPoints);

        }
    }
}