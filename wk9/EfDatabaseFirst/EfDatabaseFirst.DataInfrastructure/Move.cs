using System;
using System.Collections.Generic;

namespace EfDatabaseFirst.DataInfrastructure
{
    public partial class Move
    {
        public Move()
        {
            RoundPlayer1MoveNavigations = new HashSet<Round>();
            RoundPlayer2MoveNavigations = new HashSet<Round>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Round> RoundPlayer1MoveNavigations { get; set; }
        public virtual ICollection<Round> RoundPlayer2MoveNavigations { get; set; }
    }
}
