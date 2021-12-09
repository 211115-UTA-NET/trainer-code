using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissorsApp.Logic
{
    public class RandomMoveDecider : IMoveDecider
    {
        public Move DecideMove()
        {
            Random random = new();
            return (Move)random.Next(3); // 0, 1 or 2
        }
    }
}
