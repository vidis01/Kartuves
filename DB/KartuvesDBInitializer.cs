using System.Collections.Generic;
using System.Data.Entity;

namespace KartuvesGame.DB
{
    public class KartuvesDBInitializer : CreateDatabaseIfNotExists<KartuvesDBContext>
    {
        protected override void Seed(KartuvesDBContext context)
        {
            context.Daiktai.Add( new Daiktas { Pavadinimas = "Telefonas"});
            context.Daiktai.Add( new Daiktas { Pavadinimas = "Dviratis"});
            context.Daiktai.Add( new Daiktas { Pavadinimas = "Peilis"});
            context.Daiktai.Add( new Daiktas { Pavadinimas = "Televizorius"});
            context.Daiktai.Add( new Daiktas { Pavadinimas = "Puodelis"});
            context.Daiktai.Add( new Daiktas { Pavadinimas = "Pjuklas"});

            context.Valstybes.Add( new Valstybe { Pavadinimas = "Lenkija" });
            context.Valstybes.Add( new Valstybe { Pavadinimas = "Baltarusija" });
            context.Valstybes.Add( new Valstybe { Pavadinimas = "Rusija" });

            context.Gyvunai.Add( new Gyvunas { Pavadinimas = "Liutas"});
            context.Gyvunai.Add( new Gyvunas { Pavadinimas = "Tigras"});
            context.Gyvunai.Add( new Gyvunas { Pavadinimas = "Pele"});
            context.Gyvunai.Add( new Gyvunas { Pavadinimas = "Kate"});

            context.Miestai.Add( new Miestas { Pavadinimas = "Palanga"});
            context.Miestai.Add( new Miestas { Pavadinimas = "Klaipeda"});
            context.Miestai.Add( new Miestas { Pavadinimas = "Gargzdai"});
            context.Miestai.Add( new Miestas { Pavadinimas = "Kaunas"});
            context.Miestai.Add( new Miestas { Pavadinimas = "Jonava"});

            context.Vardai.Add( new Vardas { Pavadinimas = "Jadvyga"});
            context.Vardai.Add( new Vardas { Pavadinimas = "Algis"});
            context.Vardai.Add( new Vardas { Pavadinimas = "Jurijus"});
            context.Vardai.Add( new Vardas { Pavadinimas = "Borisas"});
            context.Vardai.Add( new Vardas { Pavadinimas = "Petras"});
            context.Vardai.Add( new Vardas { Pavadinimas = "Antanas"});
            context.Vardai.Add( new Vardas { Pavadinimas = "Jonas"});
        }
    }
}