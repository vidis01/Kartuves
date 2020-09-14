using System.ComponentModel.DataAnnotations.Schema;

namespace KartuvesGame.DB
{
    [Table("Gyvunai")]
    public class Gyvunas
    {
        public int GyvunasId { get; set; }
        public string Pavadinimas { get; set; }
        public int KiekSpeta { get; set; }
        public int KiekAtspeta { get; set; }
    }
}
