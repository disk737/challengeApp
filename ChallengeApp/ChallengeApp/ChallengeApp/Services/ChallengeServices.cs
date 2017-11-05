using ChallengeApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeApp.Services
{
    public class ChallengeServices
    {
        
        //// Esto debe venir de un servicio
        //List<Challenge> ListChallenge = new List<Challenge>
        //    {
        //        new Challenge{ChallengeName = "Explore", DescripChallenge = "Debes ir a tal lado y tomar una fotografia...", EvidenChallenge = "Foto por Email", ChallengePoint ="5"},
        //        new Challenge{ChallengeName ="Read", DescripChallenge = "Debes leer el capitulo y enviar un resumen...", EvidenChallenge = "Enviar resumen por Email", ChallengePoint = "3"},
        //        new Challenge{ChallengeName = "Demostrate", DescripChallenge = "Realiza el experimiento 13 y enviar el resultado...", EvidenChallenge = "Foto por Email", ChallengePoint = "8"}
        //    };

        private HttpClient client = new HttpClient();

        public async Task<List<Challenge>> GetAllChallenges()
        {
            List<Challenge> DataChallenge = new List<Challenge>();

            // Construyo la URI a consultar
            var uri = new Uri(string.Format(Constans.RestUrl + Constans.GetAllChallenges));

            // Indico que se realiza una peticion
            Debug.WriteLine("Peticion RefreshProjectDataAsync");

            // Hago la llamada al WS
            try
            {
                var response = await client.GetAsync(uri);

                // Espero una respuesta positiva del servidor (200)
                if (response.IsSuccessStatusCode)
                {

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

    }
}
