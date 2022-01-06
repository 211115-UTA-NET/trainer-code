using System.ComponentModel.DataAnnotations;
using RpsApi.Logic;

namespace RpsApi.Api.Dtos
{
    public class SubmittedMove
    {
        [Required]
        //[StringLength(100, MinimumLength = 5)]
        public string? Player1Name { get; set; }

        [Required]
        public string? Player2Name { get; set; }

        [Required]
        public Move Move { get; set; }

        //[Range(0, 10)] // number validated to be between 0 and 10
        //public int Number { get; set; }
    }
}
