using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeApp.Models
{

    public class ListChallenge
    {
        public List<Challenge> Challenge { get; set; }
    }

    public class Challenge
    {
        public string ChallengeID { get; set; }
        public string ChallengeName { get; set; }
        public string ChallengeDescription { get; set; }
        public string ChallengeEvidence { get; set; }
        public string ChallengePoint { get; set; }
        public string ChallengeDueDate { get; set; }
    }
}
