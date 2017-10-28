using ChallengeApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeApp.Services
{
    public class ChallengeServices
    {
        
        // Esto debe venir de un servicio
        List<Challenge> ListChallenge = new List<Challenge>
            {
                new Challenge{ChallengeName = "Explore", DescripChallenge = "Debes ir a tal lado y tomar una fotografia...", EvidenChallenge = "Foto por Email", ChallengePoint ="5"},
                new Challenge{ChallengeName ="Read", DescripChallenge = "Debes leer el capitulo y enviar un resumen...", EvidenChallenge = "Enviar resumen por Email", ChallengePoint = "3"},
                new Challenge{ChallengeName = "Demostrate", DescripChallenge = "Realiza el experimiento 13 y enviar el resultado...", EvidenChallenge = "Foto por Email", ChallengePoint = "8"}
            };

        public List<Challenge> GetAllChallenges()
        {
            return ListChallenge;
        }

    }
}
