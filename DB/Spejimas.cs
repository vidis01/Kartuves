using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace KartuvesGame.DB
{
    [Table("Spejimai")]
    public class Spejimas
    {
        public int SpejimasId { get; set; }
        public string ZaidejoVardas { get; set; }
        public string Zodis { get; set; }
        public int KiekKartuSpejo { get; set; }
        public string Spejimai { get; set; } //kokias raides ir kokius zodzius spejo
        public bool ArAtspejo { get; set; }
        public DateTime ZaidimoData { get; set; }
    }
}
