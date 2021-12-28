using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpsApi.Logic
{
    /// <summary>
    /// Decide a RPS game move randomly (equal chance of rock, paper, or scissors)
    /// </summary>
    /// <remarks>
    /// here you'd put explanations of why this class was designed the way it was
    /// </remarks>
    public class RandomMoveDecider : IMoveDecider
    {
        /// <summary>
        /// Choose the next move in a RPS game
        /// </summary>
        /// <returns>The chosen move</returns>
        public Move DecideMove()
        {
            Random random = new();
            return (Move)random.Next(3); // 0, 1 or 2
        }
    }
}
