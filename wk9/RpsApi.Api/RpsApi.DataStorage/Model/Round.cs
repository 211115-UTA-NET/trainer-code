namespace RpsApi.DataStorage.Model
{
    public class Round
    {
        public int Id { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public int? Player1Id { get; set; }
        public int? Player2Id { get; set; }

        // if you don't think you'll ever want to see the FK value directly,
        // and using the nav property will always be enough,
        // then you can leave out the FK property too
        // (EF will secretly add one anyway called a "shadow property")
        public int Player1MoveId { get; set; }
        public int Player2MoveId { get; set; }
        // (i keep them because i need to set indexes on them)

        public Move Player1Move { get; set; } = null!;
        public Player? Player1 { get; set; }
        public Move Player2Move { get; set; } = null!;
        public Player? Player2 { get; set; }
    }
}
