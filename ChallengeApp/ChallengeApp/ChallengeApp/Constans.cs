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

        // URL de la APIs Challenge
        public static string GetAllChallenges = "/challenge/leader/consultarReto";

        // URL de las APIs de listas de usuario
        public static string AcceptChallengeUser = "/challenge/user/aceptarReto";
        public static string QuitChallengeUser = "/challenge/user/renunciarReto";
        public static string GetOtherChallenges = "/challenge/user/consultarOtrosRetos";
        public static string GetUserChallenges = "/challenge/user/consultarRetos";

        // API para crear e ingresar usuarios
        public static string UserSignIn = "/challenge/user/ingresarUsuario";
        
        // Cadena que guarda el Token de seguridad
        public static string UserTokenString = "UserToken";

        // Cadenas que guardan la informacion del usuario
        public static string UserEmail = "UserEmail";       // No se esta usando
        public static string UserPassword = "UserPassword"; // No se esta usando
        public static string SaveCredentials = "SaveCredentials"; // Me indica si las credenciales estan activas o no

    }
}
