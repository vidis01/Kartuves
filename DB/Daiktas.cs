using System.ComponentModel.DataAnnotations.Schema;

namespace KartuvesGame.DB
{
    [Table("Daiktai")]
    public class Daiktas
    {
        public int DaiktasId { get; set; }
        public string Pavadinimas { get; set; }
        public int KiekSpeta { get; set; }
        public int KiekAtspeta { get; set; }
    }
}
