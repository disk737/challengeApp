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

            // Borro cualqueir arreglo guardado en memoria
            if (Application.Current.Properties.ContainsKey(Constans.AcceptChallengeUser))
            {
                // Borro el contenido de la variable
                Application.Current.Properties.Remove(Constans.AcceptChallengeUser);
            }

            if (Application.Current.Properties.ContainsKey(Constans.QuitChallengeUser))
            {
                // Borro el contenido de la variable
                Application.Current.Properties.Remove(Constans.QuitChallengeUser);
            }

            // Borro el contenido del array auxiliar si hubo un cambio en la Actividad
            if (Application.Current.Properties.ContainsKey(Constans.FlagUserList))
            {
                Application.Current.Properties.Remove(Constans.FlagUserList);
            }

            // Borro el contenido del array auxiliar si hubo un cambio en la Actividad
            if (Application.Current.Properties.ContainsKey(Constans.FlagChallengeList))
            {
                Application.Current.Properties.Remove(Constans.FlagChallengeList);
            }
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