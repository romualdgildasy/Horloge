using System;
using System.IO;

namespace Horloge
{
    public class FileLogger
    {
        public void Log(string message)
        {
            string nomFichierJournal;

            // Obtenir la date actuelle
            DateTime dateActuelle = DateTime.Now;
            string currentDateString = dateActuelle.ToString("yyyyMMdd");

            // Vérifier si c'est le week-end
            bool isWeekend = dateActuelle.DayOfWeek == DayOfWeek.Saturday || dateActuelle.DayOfWeek == DayOfWeek.Sunday;

            // Déterminer le nom du fichier de journal en fonction du jour
            if (isWeekend)
            {
                // Si c'est le week-end, utiliser "week-end.txt"
                nomFichierJournal = "weekend.txt";
            }
            else
            {
                // Sinon, utiliser "logYYYYMMDD.txt"
                nomFichierJournal = $"log{currentDateString}.txt";
            }

            // Vérifier si le fichier de journal existe
            if (!File.Exists(nomFichierJournal))
            {
                // Si le fichier n'existe pas, créer un nouveau fichier
                using (StreamWriter writer = File.CreateText(nomFichierJournal))
                {
                    writer.WriteLine($"{message} {dateActuelle:yyyy-MM-dd HH:mm:ss}");
                }
            }
            else
            {
                // Si le fichier existe, ajouter le message à la fin
                using (StreamWriter writer = File.AppendText(nomFichierJournal))
                {
                    writer.WriteLine($"{message} {dateActuelle:yyyy-MM-dd HH:mm:ss}");
                }
            }

            // Si c'est le week-end et que c'est la première connexion pour ce week-end
            if (isWeekend && dateActuelle.DayOfWeek == DayOfWeek.Saturday)
            {
                // Renommer le fichier week-end.txt en fonction de la date du samedi
                string previousWeekendFileName = $"weekend-{dateActuelle.AddDays(-1):yyyyMMdd}.txt";
                if (File.Exists("weekend.txt"))
                {
                    File.Move("weekend.txt", previousWeekendFileName);
                }

                // Créer un nouveau fichier weekend.txt pour le nouveau week-end
                using (StreamWriter writer = File.CreateText("weekend.txt"))
                {
                    writer.WriteLine($"{message} {dateActuelle:yyyy-MM-dd HH:mm:ss}");
                }
            }
        }
    }
}





