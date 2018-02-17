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

        // Metodo para obtener la lista de Challenges aceptados por el usuario
        public async Task<List<Challenge>> GetUserChallengeList()
        {
            // Capturo el Token guardado
            string userToken = Application.Current.Properties[Constans.UserTokenString].ToString();

            // Incluyo el Token de autentificacion en el encabezado
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);

            // Creo la lista para contener el resultado
            List<Challenge> DataChallenge = new List<Challenge>();

            // Construyo la URI de consulta
            var uri = new Uri(string.Format(Constans.RestUrl + Constans.GetUserChallenges));
 
            // Indico que se realiza una peticion
            Debug.WriteLine("Peticion GetChallengeList");

            // Hago la llamada al WS
            try
            {
                var response = await client.GetAsync(uri);

                // Espero una respuesta positiva del servidor (200)
                if (response.IsSuccessStatusCode)
                {
                    // Capturo la respuesta del servidor
                    var content = await response.Content.ReadAsStringAsync();

                    DataChallenge = ((ListChallenge)JsonConvert.DeserializeObject<ListChallenge>(content)).Challenge;
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return DataChallenge;
        }

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
    }
}
