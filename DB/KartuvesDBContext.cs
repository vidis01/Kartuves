using System.Data.Entity;

namespace KartuvesGame.DB
{
    public class KartuvesDBContext : DbContext
    {
        public KartuvesDBContext() : base("KartuvesDB")
        {
            Database.SetInitializer(new KartuvesDBInitializer());
        }

        public DbSet<Daiktas> Daiktai { get; set; }
        public DbSet<Gyvunas> Gyvunai { get; set; }
        public DbSet<Miestas> Miestai { get; set; }
        public DbSet<Valstybe> Valstybes { get; set; }
        public DbSet<Vardas> Vardai { get; set; }
        public DbSet<Spejimas> Spejimai { get; set; }

    }
}
