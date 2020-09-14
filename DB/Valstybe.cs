using System.ComponentModel.DataAnnotations.Schema;

namespace KartuvesGame.DB
{
    [Table("Valstybes")]
    public class Valstybe
    {
        public int ValstybeId { get; set; }
        public string Pavadinimas { get; set; }
        public int KiekSpeta { get; set; }
        public int KiekAtspeta { get; set; }
    }
}
