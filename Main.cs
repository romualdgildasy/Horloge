using System;
using System.IO;

namespace Horloge
{
    class Program
    {
        static void Main(string[] args)
        {
            FileLogger logger = new FileLogger();

            // Exemple d'utilisation du logger
            logger.Log("Test message");

            Console.WriteLine("HELLO WORD I'M ROMUALD I'M jUNUIOR DEV");
        }
    }
}