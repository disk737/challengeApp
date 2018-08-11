using ChallengeApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ChallengeApp.Services
{
    public class UserServices
    {
        // Creo el cliente Http para realizar las peticiones
        private HttpClient client = new HttpClient();

        // Metodo para obtener el Token de Logueo del usuario
        public async Task<UserToken> UserSignIn(string argUserEmail, string argUserPassword)
        {
            // Creo un onjeto para guardar la info del usuario
            var user = new User { UserEmail = argUserEmail,
                                  UserPassword = argUserPassword};

            // Creo el contenedor del Token que voy a retornar
            UserToken Token = new UserToken();

            // Genero el Body de la peticion
            var BodyRequest = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

            // Construyo la URI a consultar
            var uri = new Uri(string.Format(Constans.RestUrl + Constans.UserSignIn));

            // Hago la llamada al servicio
            try
            {
                var response = await client.PostAsync(uri, BodyRequest);

                // Leo la cadena de la respuesta
                var content = await response.Content.ReadAsStringAsync();

                // Aqui voy a recibir el token o el mensaje de error dependiendo del caso
                Token = JsonConvert.DeserializeObject<UserToken>(content);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return Token;
        }

        // Metodo para hacer Logout y borrar el token del telefono
        public void UserLogout()
        {
            // Verifico que la Key exista dentro del telefono
            if (Application.Current.Properties.ContainsKey(Constans.SaveCredentials))
            {
                // Cambio el estado de SaveCredentials a Inactivo
                Application.Current.Properties[Constans.SaveCredentials] = Constans.SaveUnactive;
            }

            // Si el Token se encuentra en el telefono lo debo de borrar
            if (Application.Current.Properties.ContainsKey(Constans.UserTokenString))
            {
                // Borro el contenido de la variable que contiene el Token
                Application.Current.Properties.Remove(Constans.UserTokenString);
            }

        }
    }
}
