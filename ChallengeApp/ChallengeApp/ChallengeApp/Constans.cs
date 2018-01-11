using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeApp
{
    class Constans
    {
        public static string RestUrl = "https://veros-challenge-app.herokuapp.com";

        // URL de la API Challenge
        public static string GetAllChallenges = "/challenge/leader/consultarReto";
        public static string UserSignIn = "/challenge/user/ingresarUsuario";

        // Cadena que guarda el Token de seguridad
        public static string UserTokenString = "UserToken";
    }
}
