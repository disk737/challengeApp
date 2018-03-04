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
    public partial class MainTabView : TabbedPage
    {
        public MainTabView()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            string userToken = Application.Current.Properties[Constans.UserTokenString].ToString();

            DisplayAlert("Challenge", userToken, "OK");
                // Guardo el token generado para el usuario
                 
        }
    }
}