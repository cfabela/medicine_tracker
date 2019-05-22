﻿using System;
using System.IO;

using MedicineTracker.Database;
using MedicineTracker.Droid.Database;

using SQLite;
using Xamarin.Forms;

//Android Implementation of the IDInterface CreateConnection method in the Android Project

[assembly: Dependency(typeof(DatabaseService))]
namespace MedicineTracker.Droid.Database
{
    public class DatabaseService : IDBInterface
    {
        public DatabaseService()
        {
        }

        public SQLiteConnection CreateConnection()
        {
            var sqliteFilename = "MedicineTracker.db";
            string documentsDirectoryPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsDirectoryPath, sqliteFilename);

            if (!File.Exists(path))
            {
                using(var binaryReader = new BinaryReader(Android.App.Application.Context.Assets.Open(sqliteFilename)))
                {
                    using (var binaryWriter = new BinaryWriter(new FileStream(path, FileMode.Create)))
                    {
                        byte[] buffer = new byte[2048];
                        int lenght = 0;
                        while((lenght = binaryReader.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            binaryWriter.Write(buffer, 0, lenght);
                        }
                    }
                }
            }

            var conn = new SQLiteConnection(path);
            return conn;
        }

        private void ReadWriteStream(Stream readStream, Stream writeStream)
        {
            int Length = 256;
            Byte[] buffer = new Byte[Length];
            int bytesRead = readStream.Read(buffer, 0, Length);
            while(bytesRead >= 0)
            {
                writeStream.Write(buffer, 0, bytesRead);
                bytesRead = readStream.Read(buffer, 0, Length);
            }

            readStream.Close();
            writeStream.Close();
        }
    }
}
