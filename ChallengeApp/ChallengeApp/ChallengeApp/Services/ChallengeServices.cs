﻿using ChallengeApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ChallengeApp.Services
{
    public class ChallengeServices
    {
        //TODO: Extraer el sistema de autentificacion en un metodo aparte

        private HttpClient client = new HttpClient();

        public async Task<List<Challenge>> GetAllChallenges()
        {
            List<Challenge> DataChallenge = new List<Challenge>();

            // Capturo el Token guardado en la aplicacion
            string userToken = Application.Current.Properties[Constans.UserTokenString].ToString();

            // Incluyo el Token de autentificacion en el encabezado
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);

            // Construyo la URI a consultar
            var uri = new Uri(string.Format(Constans.RestUrl + Constans.GetOtherChallenges));

            // Indico que se realiza una peticion
            Debug.WriteLine("Peticion GetOtherChallenges");

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

        // Metodo que se encarga de ACEPTAR el reto
        public async Task<bool> AcceptChallengeUser(Challenge argChallenge)
        {
            // Creo la variable para la respuesta del servicio
            bool ServiceResponse = false;

            // Capturo el Token guardado
            string userToken = Application.Current.Properties[Constans.UserTokenString].ToString();

            // Incluyo el Token de autentificacion en el encabezado
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);

            // Construyo la URI a consultar
            var uri = new Uri(string.Format(Constans.RestUrl + Constans.AcceptChallengeUser));

            // Debo enviar el Challenge aceptado para que sea enviado en el Body
            var content = JsonConvert.SerializeObject(argChallenge);

            // Indico que se realiza una peticion
            Debug.WriteLine("Peticion AcceptChallengeUser");

            // Hago la llamada al WS
            try
            {
                var response = await client.PostAsync(uri, new StringContent(content, Encoding.UTF8, "application/json"));

                // Espero una respuesta positiva del servidor (200)
                if (response.IsSuccessStatusCode)
                {
                    // Capturo la respuesta del servidor
                    //var content = await response.Content.ReadAsStringAsync();

                    // Indicio que tuvimos una respuesta del servidor
                    ServiceResponse = true;

                    // Indico que se realiza una peticion
                    Debug.WriteLine("Reto Aceptado con Exito");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return ServiceResponse;
        }

        // Metodo que se encarga de RENUNCIAR al reto
        public async Task<bool> QuitChallengeUser(Challenge argChallenge)
        {
            // Creo la variable para la respuesta del servicio
            bool ServiceResponse = false;

            // Capturo el Token guardado
            string userToken = Application.Current.Properties[Constans.UserTokenString].ToString();

            // Incluyo el Token de autentificacion en el encabezado
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);

            // Debo enviar el Challenge aceptado para que sea enviado en el Body
            //var content = JsonConvert.SerializeObject(argChallenge);

            // Construyo la URI a consultar
            var uri = new Uri(string.Format(Constans.RestUrl + Constans.QuitChallengeUser + "/" + argChallenge.ChallengeID));

            // Indico que se realiza una peticion a la consola 
            Debug.WriteLine("Peticion QuitChallengeUser");

            // Hago la llamada al WS
            try
            {
                // Llamo el metodo encargado de ejecutar la renuncia al reto.
                var response = await client.DeleteAsync(uri);

                // Espero una respuesta positiva del servidor (200)
                if (response.IsSuccessStatusCode)
                {
                    // Capturo la respuesta del servidor
                    //var content = await response.Content.ReadAsStringAsync();

                    // Indicio que tuvimos una respuesta del servidor
                    ServiceResponse = true;

                    // Indico que se realiza una peticion
                    Debug.WriteLine("Reto renunciado con Exito");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return ServiceResponse;
        }

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

    }
}
