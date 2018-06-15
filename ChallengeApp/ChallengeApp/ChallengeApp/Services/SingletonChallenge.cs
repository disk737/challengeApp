using ChallengeApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeApp.Services
{
    public class SingletonChallenge
    {
        private static SingletonChallenge instance = null;

        protected SingletonChallenge() { }

        // Aqui pongo los Array que necesito

        public ObservableCollection<Challenge> _obsListChallenge { get; set; }
        public ObservableCollection<Challenge> _obsUserListChallenge { get; set; }


        public static SingletonChallenge Instance
        {
            get
            {
                if (instance == null)
                    instance = new SingletonChallenge();

                return instance;

            }
        }
    }
}
