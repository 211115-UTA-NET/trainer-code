using System;
using System.Collections.Generic;

namespace RpsApi.DataStorage.Model
{
    public partial class Round
    {
        public int Id { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public int? Player1 { get; set; }
        public int? Player2 { get; set; }
        public int Player1Move { get; set; }
        public int Player2Move { get; set; }

        public virtual Move Player1MoveNavigation { get; set; } = null!;
        public virtual Player? Player1Navigation { get; set; }
        public virtual Move Player2MoveNavigation { get; set; } = null!;
        public virtual Player? Player2Navigation { get; set; }
    }
}
