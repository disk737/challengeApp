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
    public partial class DetailChallengeView : ContentPage
    {
        public DetailChallengeView(Challenge challenge)
        {
            InitializeComponent();

            BindingContext = challenge;

            // No se si esta sea la mejor manera de mostrar el puntaje
            UserInfo userInfo = new UserInfo { UserPoints = "25" };

            LabelPoints.Text = String.Format("Points: {0}", userInfo.UserPoints);
        }

        async private void AcceptChallengeHandler(object sender, EventArgs e)
        {
            await DisplayAlert("Challenge", "I Accept Your Challenge", "OK");

        }
    }
}