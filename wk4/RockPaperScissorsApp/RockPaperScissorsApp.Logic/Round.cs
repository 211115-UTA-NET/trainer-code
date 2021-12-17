namespace RockPaperScissorsApp.Logic
{
    public class Round
    {
        // XmlSerializer (and often other serializers)
        // expect classes that have a zero-argument constructor
        //    and they can fill in public properties via their setters.
        //  a class that ONLY has a zero-arg constructor and public get set properties is sometimes
        //    called a POCO (plain old CLR object)


        // C# has "attributes" - they don't do anything by themselves,
        // they're there for some other code to notice them and make some change to their own behavior

        // ex: [ 10/7 Computer: Rock VS You: Paper => You Win! ]
        public DateTime Date { get; }

        public Move Player1 { get; }
        public Move Player2 { get; }
        public RoundResult Result => EvaluateResult(Player1, Player2);

        // constructor
        public Round(DateTime date, Move player1, Move player2)
        {
            Date = date;
            Player1 = player1;
            Player2 = player2;
        }

        // assigns the CPU move to player 1

        // result from perspective of player 1
        public static RoundResult EvaluateResult(Move player1, Move player2)
        {
            // C# lately supports nice things
            //  pattern matching syntax, switch expression, tuples
            return (player1, player2) switch
            {
                (Move.Rock, Move.Scissors) => RoundResult.Win,
                (Move.Scissors, Move.Rock) => RoundResult.Loss,
                (Move.Paper, Move.Rock) => RoundResult.Win,
                (Move.Rock, Move.Paper) => RoundResult.Loss,
                (Move.Scissors, Move.Paper) => RoundResult.Win,
                (Move.Paper, Move.Scissors) => RoundResult.Loss,
                (var m, var m2) when m == m2 => RoundResult.Tie,
                _ => throw new InvalidOperationException(),
            };
        }
    }
}
