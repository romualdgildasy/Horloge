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

            // V�rifier si c'est le week-end
            bool isWeekend = dateActuelle.DayOfWeek == DayOfWeek.Saturday || dateActuelle.DayOfWeek == DayOfWeek.Sunday;

            // D�terminer le nom du fichier de journal en fonction du jour
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

            // V�rifier si le fichier de journal existe
            if (!File.Exists(nomFichierJournal))
            {
                // Si le fichier n'existe pas, cr�er un nouveau fichier
                using (StreamWriter writer = File.CreateText(nomFichierJournal))
                {
                    writer.WriteLine($"{message} {dateActuelle:yyyy-MM-dd HH:mm:ss}");
                }
            }
            else
            {
                // Si le fichier existe, ajouter le message � la fin
                using (StreamWriter writer = File.AppendText(nomFichierJournal))
                {
                    writer.WriteLine($"{message} {dateActuelle:yyyy-MM-dd HH:mm:ss}");
                }
            }

            // Si c'est le week-end et que c'est la premi�re connexion pour ce week-end
            if (isWeekend && dateActuelle.DayOfWeek == DayOfWeek.Saturday)
            {
                // Renommer le fichier week-end.txt en fonction de la date du samedi
                string previousWeekendFileName = $"weekend-{dateActuelle.AddDays(-1):yyyyMMdd}.txt";
                if (File.Exists("weekend.txt"))
                {
                    File.Move("weekend.txt", previousWeekendFileName);
                }

                // Cr�er un nouveau fichier weekend.txt pour le nouveau week-end
                using (StreamWriter writer = File.CreateText("weekend.txt"))
                {
                    writer.WriteLine($"{message} {dateActuelle:yyyy-MM-dd HH:mm:ss}");
                }
            }
        }
    }
}





