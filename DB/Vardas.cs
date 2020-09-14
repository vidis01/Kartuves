using System.ComponentModel.DataAnnotations.Schema;

namespace KartuvesGame.DB
{
    [Table("Vardai")]
    public class Vardas
    {
        public int VardasId { get; set; }
        public string Pavadinimas { get; set; }
        public int KiekSpeta { get; set; }
        public int KiekAtspeta { get; set; }
    }
}
