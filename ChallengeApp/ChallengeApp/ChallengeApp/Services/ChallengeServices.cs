using ChallengeApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace ChallengeApp.Services
{
    public class ChallengeServices
    {

        private HttpClient client = new HttpClient();

        public async Task<List<Challenge>> GetAllChallenges()
        {
            List<Challenge> DataChallenge = new List<Challenge>();

            // Construyo la URI a consultar
            var uri = new Uri(string.Format(Constans.RestUrl + Constans.GetAllChallenges));

            // Indico que se realiza una peticion
            Debug.WriteLine("Peticion GetAllChallenges");

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
