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

            // No se si esta sea la mejor manera de mostrar el puntaje
            User userInfo = new User { UserPoints = "25" };

            LabelPoints.Text = String.Format("Points: {0}", userInfo.UserPoints);

        }

        async private void ListChallenge_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var challenge = e.SelectedItem as Challenge;

            await Navigation.PushAsync(new DetailChallengeView(challenge));
        }
    }
}