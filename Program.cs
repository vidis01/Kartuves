using KartuvesGame.DB;
using System;
using System.Linq;

namespace KartuvesGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Hangman.Kartuves();

            //using (var db = new KartuvesDBContext())
            //{
            //    db.Daiktai.Any();
            //    db.Daiktai.Count();
            //}

            //------------------------------------------
            Console.WriteLine();
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
