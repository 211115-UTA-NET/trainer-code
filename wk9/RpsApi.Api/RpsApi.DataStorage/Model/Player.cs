using System;
using System.Collections.Generic;

namespace RpsApi.DataStorage.Model
{
    public partial class Player
    {
        public Player()
        {
            RoundPlayer1Navigations = new HashSet<Round>();
            RoundPlayer2Navigations = new HashSet<Round>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Round> RoundPlayer1Navigations { get; set; }
        public virtual ICollection<Round> RoundPlayer2Navigations { get; set; }
    }
}
