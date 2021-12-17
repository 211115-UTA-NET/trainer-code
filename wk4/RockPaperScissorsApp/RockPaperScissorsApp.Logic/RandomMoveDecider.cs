using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissorsApp.Logic
{
    /// <summary>
    /// Decide a RPS game move randomly (equal chance of rock, paper, or scissors)
    /// </summary>
    public class RandomMoveDecider : IMoveDecider
    {
        public Move DecideMove()
        {
            Random random = new();
            return (Move)random.Next(3); // 0, 1 or 2
        }
    }
}
