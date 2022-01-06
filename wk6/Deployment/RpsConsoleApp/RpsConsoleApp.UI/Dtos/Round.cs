namespace RpsConsoleApp.UI.Dtos
{
    public class Round
    {
        public DateTimeOffset Date { get; set; }
        public int Player1 { get; set; }
        public int Player2 { get; set; }
        public int Result { get; set; }
    }
}
