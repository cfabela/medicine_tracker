using System;
using System.IO;
using Foundation;
using MedicineTracker.Database;
using MedicineTracker.iOS.Database;

using SQLite;
using Xamarin.Forms;

//iOS Implementation of the IDBInterfae CreateConnection method in
//the iOS Project

[assembly: Dependency(typeof(DatabaseService))]
namespace MedicineTracker.iOS.Database
{
    public class DatabaseService : IDBInterface
    {
        public DatabaseService()
        {
        }

        public SQLiteConnection  CreateConnection()
        {
            var sqliteFilename = "MedicineTracker.db";
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }

            string path = Path.Combine(libFolder, sqliteFilename);
            System.Diagnostics.Debug.WriteLine(path);

            var connection = new SQLiteConnection(path);
            return connection;

        }

    }
}
