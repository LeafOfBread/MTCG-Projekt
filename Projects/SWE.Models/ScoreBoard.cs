using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE.Models
{
    internal class ScoreBoard
    {
        public ScoreBoard(int TotalPlayers, int TotalBattles, List<User> users)
        {
            this.TotalPlayers = TotalPlayers;
            this.TotalBattles = TotalBattles;
            this.users = users;
        }

        public int TotalPlayers;
        public int TotalBattles;

        public List<User> users;
    }
}
