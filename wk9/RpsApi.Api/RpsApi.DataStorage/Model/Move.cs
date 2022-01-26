namespace RpsApi.DataStorage.Model
{
    public class Move
    {
        public Move(string name)
        {
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }

        // you don't need to put any navigation properties you know you won't need to ever use
        // you'll need at least ONE of the two, so that EF can recognize the FK relationship

        // .... except due to a variety of reasons
        public ICollection<Round> RoundsUsedByPlayer1 { get; set; }
        public ICollection<Round> RoundsUsedByPlayer2 { get; set; }
    }
}
