using KartuvesGame.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KartuvesGame
{
    public class Hangman
    {
        public static void Kartuves()
        {
            using (var db = new KartuvesDBContext())
            {
                List<string> vardai = new List<string> { "INGA", "JONAS", "ANTANAS", "IGNAS" };
                List<string> lietuvosMiestai = new List<string> { "VILNIUS", "KAUNAS", "BALBIERISKIS", "KLAIPEDA" };
                List<string> valstybes = new List<string> { "VOKIETIJA", "ITALIJA", "LIETUVA", "PRANCUZIJA" };
                List<string> gyvunai = new List<string> { "TIGRAS", "HIPOPOTAMAS", "LIUTAS", "SUO", "KATE" };

                bool zaisti = true;

                while (zaisti)
                {
                    List<char> spetosRaides = new List<char>();
                    List<string> spetiZodziai = new List<string>();

                    var rand = new Random();

                    var pasirinkimas = ZmogusPasirenkaZodziuGrupe(out string zodziuGrupe);

                    var zodis = ZodzioParinkimas(pasirinkimas, vardai, lietuvosMiestai, valstybes, gyvunai, db, rand);

                    while (zodis == "-1")
                    {
                        Console.WriteLine($"Zodziu grupeje {zodziuGrupe} spejote visus zodzius. Pasirinkite kita grupe;");

                        pasirinkimas = ZmogusPasirenkaZodziuGrupe(out zodziuGrupe);
                        zodis = ZodzioParinkimas(pasirinkimas, vardai, lietuvosMiestai, valstybes, gyvunai, db, rand);
                    }

                    char[] zodisCharArray = GetEmptyZodisArray(zodis.Length);

                    bool spejimaiBaigti = false;
                    int klaidos = 0;

                    while (!spejimaiBaigti)
                    {
                        Console.Clear();
                        KartuviuPaveikslelis(klaidos);

                        Console.WriteLine($"Spejamas zodis is grupes: {zodziuGrupe}.");
                        PrintCharAray(zodisCharArray);

                        PrintSpetiZodziaiIrRaides(spetosRaides, spetiZodziai);

                        Console.WriteLine("Spekite raide arba visa zodi:");
                        var spejimas = Console.ReadLine().ToUpper();

                        while (!(ArGerasSpejimas(spejimas) &&
                            ArNesikartojaSpejimas(spejimas, spetosRaides, spetiZodziai)))
                        {
                            Console.WriteLine("Spekite raide arba visa zodi:");
                            spejimas = Console.ReadLine().ToUpper();
                        }

                        IsimintiSpejima(spejimas, spetosRaides, spetiZodziai);

                        if (!SpejimoAnalize(spejimas, zodis, zodisCharArray))
                        {
                            klaidos++;
                        }

                        if (klaidos == 7)
                        {
                            Console.Clear();
                            KartuviuPaveikslelis(klaidos);
                            PrintCharAray(zodisCharArray);
                            PrintSpetiZodziaiIrRaides(spetosRaides, spetiZodziai);
                            Console.WriteLine();
                            Console.WriteLine("JUS PRALAIMEJOTE =( !");
                            spejimaiBaigti = true;
                        }
                        else if (ArLaimejo(zodisCharArray))
                        {
                            Console.Clear();
                            KartuviuPaveikslelis(klaidos);
                            PrintCharAray(zodisCharArray);
                            PrintSpetiZodziaiIrRaides(spetosRaides, spetiZodziai);
                            Console.WriteLine();
                            Console.WriteLine("JUS LAIMEJOTE =) !");
                            spejimaiBaigti = true;
                        }

                    }

                    zaisti = ArNoriteToliauZaisti();

                    Console.Clear();
                }

            }
        }

        static bool ArNoriteToliauZaisti()
        {
            Console.WriteLine("Ar norite zaisti dar karta? y/n");
            string ivestis = Console.ReadLine().ToLower();

            while (ivestis != "y" && ivestis != "n")
            {
                Console.Clear();
                Console.WriteLine("Ar norite zaisti dar karta? y/n");
                ivestis = Console.ReadLine().ToLower();
            }

            if (ivestis == "y")
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        static bool ArLaimejo(char[] zodisCharArray)
        {
            for (int i = 0; i < zodisCharArray.Length; i++)
            {
                if (zodisCharArray[i] == '_')
                {
                    return false;
                }
            }
            return true;
        }

        static void PrintSpetiZodziaiIrRaides(List<char> spetosRaides, List<string> spetiZodziai)
        {
            Console.WriteLine();
            Console.WriteLine("Spetos raides:");
            foreach (var raide in spetosRaides)
            {
                Console.Write($"{raide}, ");
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Speti zodziai:");
            foreach (var zodis in spetiZodziai)
            {
                Console.Write($"{zodis}, ");
            }
            Console.WriteLine();
            Console.WriteLine();
        }

        static bool ArNesikartojaSpejimas(string spejimas, List<char> spetosRaides, List<string> spetiZodziai)
        {
            if (spejimas.Length == 1)
            {
                for (int i = 0; i < spetosRaides.Count; i++)
                {
                    if (spejimas[0] == spetosRaides[i])
                    {
                        Console.WriteLine($"Raide {spejimas[0]} jau buvo speta.");
                        return false;
                    }
                }
            }
            else
            {
                for (int i = 0; i < spetiZodziai.Count; i++)
                {
                    if (spejimas == spetiZodziai[i])
                    {
                        Console.WriteLine($"Zodis {spejimas} jau buvo spetas.");
                        return false;
                    }
                }
            }
            return true;
        }

        static bool ArGerasSpejimas(string spejimas)
        {
            if (spejimas.Length == 1)
            {
                if (spejimas[0] >= 65 && spejimas[0] <= 90)
                {
                    return true;
                }
                else
                {
                    Console.WriteLine($"Simbolis {spejimas[0]} nera raide!");
                    return false;
                }
            }
            else
            {
                for (int i = 0; i < spejimas.Length; i++)
                {
                    if (!(spejimas[i] >= 65 && spejimas[i] <= 90))
                    {
                        Console.WriteLine($"{spejimas} nera zodis!");
                        return false;
                    }
                }
            }
            return true;
        }

        static void IsimintiSpejima(string spejimas, List<char> spetosRaides, List<string> spetiZodziai)
        {
            if (spejimas.Length == 1)
            {
                spetosRaides.Add(spejimas[0]);
            }
            else
            {
                spetiZodziai.Add(spejimas);
            }
        }

        static bool SpejimoAnalize(string spejimas, string zodis, char[] zodisCharArray)
        {
            bool arAtspejo = false;

            if (spejimas.Length == 1)
            {
                for (int i = 0; i < zodis.Length; i++)
                {
                    if (zodis[i] == spejimas[0])
                    {
                        zodisCharArray[i] = spejimas[0];
                        arAtspejo = true;
                    }
                }
            }
            else
            {
                if (zodis == spejimas)
                {
                    arAtspejo = true;

                    for (int i = 0; i < zodis.Length; i++)
                    {
                        zodisCharArray[i] = zodis[i];
                    }
                }
            }

            return arAtspejo;
        }

        static void PrintCharAray(char[] zodisCharArray)
        {
            string spejamasZodis = string.Empty;

            for (int i = 0; i < zodisCharArray.Length; i++)
            {
                spejamasZodis += $"{zodisCharArray[i]} ";
            }

            Console.WriteLine(spejamasZodis);
        }

        static char[] GetEmptyZodisArray(int masyvoDydis)
        {
            char[] zodisCharArray = new char[masyvoDydis];

            for (int i = 0; i < zodisCharArray.Length; i++)
            {
                zodisCharArray[i] = '_';
            }

            return zodisCharArray;
        }

        static string ZodzioParinkimas(
            int pasirinkimas,
            List<string> vardai,
            List<string> lietuvosMiestai,
            List<string> valstybes,
            List<string> gyvunai,
            KartuvesDBContext db,
            Random rand)
        {
            string zodis = string.Empty;

            switch (pasirinkimas)
            {
                case 1:
                    //if (vardai.Count == 0)
                    //{
                    //    zodis = "-1";
                    //}
                    //else
                    //{
                    //    zodis = vardai[rand.Next(0, vardai.Count)];
                    //    vardai.Remove(zodis);
                    //}
                    var zodisId = rand.Next(1, db.Vardai.Count());
                    zodis = db.Vardai.First(v => v.VardasId == zodisId).Pavadinimas;
                    break;
                case 2:
                    if (lietuvosMiestai.Count == 0)
                    {
                        zodis = "-1";
                    }
                    else
                    {
                        zodis = lietuvosMiestai[rand.Next(0, lietuvosMiestai.Count)];
                        lietuvosMiestai.Remove(zodis);
                    }
                    break;
                case 3:
                    if (valstybes.Count == 0)
                    {
                        zodis = "-1";
                    }
                    else
                    {
                        zodis = valstybes[rand.Next(0, valstybes.Count)];
                        valstybes.Remove(zodis);
                    }
                    break;
                case 4:
                    if (gyvunai.Count == 0)
                    {
                        zodis = "-1";
                    }
                    else
                    {
                        zodis = gyvunai[rand.Next(0, gyvunai.Count)];
                        gyvunai.Remove(zodis);
                    }
                    break;
                default:
                    break;
            }

            return zodis;
        }

        static int ZmogusPasirenkaZodziuGrupe(out string zodziuGrupe)
        {
            var pasirinkimas = ReadIntNumber();

            switch (pasirinkimas)
            {
                case 1:
                    zodziuGrupe = "VARDAI";
                    break;
                case 2:
                    zodziuGrupe = "LIETUVOS MIESTAI";
                    break;
                case 3:
                    zodziuGrupe = "VALSTYBES";
                    break;
                case 4:
                    zodziuGrupe = "GYVUNAI";
                    break;
                case 5:
                    zodziuGrupe = "DAIKTAI";
                    break;
                default:
                    zodziuGrupe = "";
                    break;
            }

            return pasirinkimas;
        }

        static int ReadIntNumber()
        {
            int skaicius;

            Console.WriteLine("Pasirinkite is kokios zodziu grupes norite zodzio:");
            Console.WriteLine("1 - VARDAI");
            Console.WriteLine("2 - LIETUVOS MIESTAI");
            Console.WriteLine("3 - VALSTYBES");
            Console.WriteLine("4 - GYVUNAI");
            Console.WriteLine("5 - DAIKTAI");

            while (!(int.TryParse(Console.ReadLine(), out skaicius) &&
                skaicius >= 1 && skaicius <= 5))
            {
                Console.Clear();
                Console.WriteLine("Blogas pasirinkimas.");
                Console.WriteLine();
                Console.WriteLine("Pasirinkite is kokios zodziu grupes norite zodzio:");
                Console.WriteLine("1 - VARDAI");
                Console.WriteLine("2 - LIETUVOS MIESTAI");
                Console.WriteLine("3 - VALSTYBES");
                Console.WriteLine("4 - GYVUNAI");
                Console.WriteLine("5 - DAIKTAI");
            }

            return skaicius;
        }

        static void KartuviuPaveikslelis(int a)
        {
            switch (a)
            {
                case 0:
                    Console.WriteLine(@" ------|");
                    Console.WriteLine(@"| /");
                    Console.WriteLine(@"|/");
                    Console.WriteLine(@"|");
                    Console.WriteLine(@"|");
                    Console.WriteLine(@"|");
                    Console.WriteLine(@"|");
                    Console.WriteLine(@"|");
                    Console.WriteLine(@"----");
                    break;
                case 1:
                    Console.WriteLine(@" ------|");
                    Console.WriteLine(@"| /    o");
                    Console.WriteLine(@"|/");
                    Console.WriteLine(@"|");
                    Console.WriteLine(@"|");
                    Console.WriteLine(@"|");
                    Console.WriteLine(@"|");
                    Console.WriteLine(@"|");
                    Console.WriteLine(@"----");
                    break;
                case 2:
                    Console.WriteLine(@" ------|");
                    Console.WriteLine(@"| /    o");
                    Console.WriteLine(@"|/     |");
                    Console.WriteLine(@"|");
                    Console.WriteLine(@"|");
                    Console.WriteLine(@"|");
                    Console.WriteLine(@"|");
                    Console.WriteLine(@"|");
                    Console.WriteLine(@"----");
                    break;
                case 3:
                    Console.WriteLine(@" ------|");
                    Console.WriteLine(@"| /    o");
                    Console.WriteLine(@"|/     | ");
                    Console.WriteLine(@"|      O");
                    Console.WriteLine(@"|");
                    Console.WriteLine(@"|");
                    Console.WriteLine(@"|");
                    Console.WriteLine(@"|");
                    Console.WriteLine(@"----");
                    break;
                case 4:
                    Console.WriteLine(@" ------|");
                    Console.WriteLine(@"| /    o");
                    Console.WriteLine(@"|/    \|");
                    Console.WriteLine(@"|      O");
                    Console.WriteLine(@"|");
                    Console.WriteLine(@"|");
                    Console.WriteLine(@"|");
                    Console.WriteLine(@"|");
                    Console.WriteLine(@"----");
                    break;
                case 5:
                    Console.WriteLine(@" ------|");
                    Console.WriteLine(@"| /    o");
                    Console.WriteLine(@"|/    \|/");
                    Console.WriteLine(@"|      O");
                    Console.WriteLine(@"|");
                    Console.WriteLine(@"|");
                    Console.WriteLine(@"|");
                    Console.WriteLine(@"|");
                    Console.WriteLine(@"----");
                    break;
                case 6:
                    Console.WriteLine(@" ------|");
                    Console.WriteLine(@"| /    o");
                    Console.WriteLine(@"|/    \|/");
                    Console.WriteLine(@"|      O");
                    Console.WriteLine(@"|     /");
                    Console.WriteLine(@"|");
                    Console.WriteLine(@"|");
                    Console.WriteLine(@"|");
                    Console.WriteLine(@"----");
                    break;
                case 7:
                    Console.WriteLine(@" ------|");
                    Console.WriteLine(@"| /    o");
                    Console.WriteLine(@"|/    \|/");
                    Console.WriteLine(@"|      O");
                    Console.WriteLine(@"|     / \");
                    Console.WriteLine(@"|");
                    Console.WriteLine(@"|");
                    Console.WriteLine(@"|");
                    Console.WriteLine(@"----");
                    break;
                default:
                    break;
            }


        }
    }
}

