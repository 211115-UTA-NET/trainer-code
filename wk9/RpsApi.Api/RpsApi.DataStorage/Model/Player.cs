using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RpsApi.DataStorage.Model
{
    public class Player
    {
        public Player(string username) // EF can handle simple constructors like this
        {
            Username = username;
        }

        //[Key] // set something as primary key (not necessary in this case b/c convention can discover this property as PK)
        public int Id { get; set; }

        //[Required] // NOT NULL (or, just use nullable / non-nullable type for the property itself)
        //[MaxLength(30)] // NVARCHAR(30) instead of NVARCHAR(MAX) (default)
        public string Username { get; set; }


        public ICollection<Round> RoundsAsPlayer1 { get; set; } = new HashSet<Round>();
        public ICollection<Round> RoundsAsPlayer2 { get; set; } = new HashSet<Round>();
    }
}
