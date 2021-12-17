namespace RockPaperScissorsApp.Logic
{
    public interface IMoveDecider
    {
        /// <summary>
        /// Choose the next move in a RPS game
        /// </summary>
        /// <returns>The chosen move</returns>
        public Move DecideMove();
    }
}
