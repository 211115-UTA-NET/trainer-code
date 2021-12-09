namespace RockPaperScissorsApp.Logic
{
    public class RepetitiveMoveDecider : IMoveDecider
    {
        private Move _previousMove = Move.Paper;

        public Move DecideMove()
        {
            Move newMove = _previousMove switch
            {
                Move.Paper => Move.Rock,
                Move.Rock => Move.Scissors,
                Move.Scissors => Move.Paper,
                _ => throw new InvalidOperationException() // impossible
            };
            _previousMove = newMove;
            return newMove;
        }
    }
}
