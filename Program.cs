using System;
using System.IO.Compression;
using System.Collections.Generic;

namespace FileSyncronation
{
    class Program
    {
        //Variablendeklaration
        static List<string> fileList = new List<string>();

        static void Main(string[] args)
        {

            ///Variablendeklaration
            string fileName;
            string destFile;
            string zipFileName;
            string quellOrdner;
            string zielOrdner;
            string createdfileName;
            string[] folderNames = new string[2];
            bool sourceFolderExists = false;
            string destinationFolderName;
            ///Eingabeabfrage            
            folderNames = folderNameInteraction();
            quellOrdner = folderNames[0];
            zielOrdner = folderNames[1];

            ///Kreiert Ordnername
            createdfileName = createFileName();

            ///Erstellt Ordner in zielOrdner
            destinationFolderName = zielOrdner + createdfileName;
            System.IO.Directory.CreateDirectory(destinationFolderName);

            ///Überprüft ob Quellordner Existiert
            if (System.IO.Directory.Exists(quellOrdner))
            {
                sourceFolderExists = true;
            }

            ///Einlesen der Dateien
            List<string> files = getFiles(quellOrdner);

            if (sourceFolderExists == true)
            {
                foreach (var file in files)
                {
                    Console.WriteLine(file+"\n");
                    
                    fileName = System.IO.Path.GetFileName(file);
                    destFile = System.IO.Path.Combine(destinationFolderName, fileName);
                    System.IO.File.Copy(file, destFile, true);
                }
            }
            
            
            zipFileName = (destinationFolderName + ".zip");

            if (System.IO.Directory.Exists(destinationFolderName))
            {
                ZipFile.CreateFromDirectory(destinationFolderName, zipFileName);
            }

            
        }

        public static List<string> getFiles(string folderName) {

            string[] files = System.IO.Directory.GetFiles(folderName);
            string[] folders = System.IO.Directory.GetDirectories(folderName);
            foreach (var folder in folders)
            {
                //Console.WriteLine(folder);
                getFiles(folder);
            }

            foreach (var file in files)
            {
                //Console.WriteLine(file);
                fileList.Add(file);
            }
            return fileList;
        }

        public static string createFileName() {
            string dateTimeNow = DateTime.Now.ToString();
            string[] datTimeNowToStringWithoutPoints = new string[3];
            string createdfileName;

            string[] dateTimeNowTime = dateTimeNow.Split(" ");
            datTimeNowToStringWithoutPoints = dateTimeNowTime[0].Split(".");
            dateTimeNow = datTimeNowToStringWithoutPoints[0] + datTimeNowToStringWithoutPoints[1] + datTimeNowToStringWithoutPoints[2];


            ///Dateipfade werden hier eingeschrieben
            createdfileName = $@"\FileSync{dateTimeNow}";


            return createdfileName;
        }

        public static string[] folderNameInteraction(){

            string[] folderNames = new string[2];
            string quellOrdner;
            string zielOrdner;

        Console.WriteLine("Gib einen Quellordner an");
            quellOrdner = Console.ReadLine();
            Console.WriteLine("Wenn sie fortfahren wollen drücken sie bitte eine beliebige Taste");
            Console.ReadKey();
            Console.WriteLine("Geben sie einen Zielordner an");
            zielOrdner = Console.ReadLine();
            Console.WriteLine("Drücken sie eine beliebige Taste zum starten.");
            Console.ReadKey();

            folderNames[0] = quellOrdner;
            folderNames[1] = zielOrdner;
            return folderNames;
        }
    }
}
