using System;
using System.Collections.Generic;
using System.Linq;

using MedicineTracker.Database;
using MedicineTracker.Models;

using SQLite;

using Xamarin.Forms;

namespace MedicineTracker.Droid.Database
{
    public class Database
    {
        private static object locker = new object();
        private static SQLiteConnection database;

        public Database()
        {
            database = DependencyService.Get<IDBInterface>().CreateConnection();

            //Create the tables
            database.CreateTable<MedicineItem>();
        }

        //Gets all of the medicine items from our database
        public IEnumerable<MedicineItem> GetItems()
        {
            lock (locker)
            {
                return (from i in database.Table<MedicineItem>() select i).ToList();
            }
        }

        //Gets a specific medicine item from the database
        public MedicineItem GetItem(int id)
        {
            lock (locker)
            {
                return database.Table<MedicineItem>().FirstOrDefault(x => x.Id == id);
            }
        }

        public int SaveItem(MedicineItem item)
        {
            lock (locker)
            {
                if(item.Id != 0)
                {
                    database.Update(item);
                    return item.Id;
                }
                else
                {
                    return database.Insert(item);
                }
            }
        }

        //Deletes a specific medicine item from the database...
        public int DeleteItem(int id)
        {
            lock (locker)
            {
                return database.Delete<MenuItem>(id);
            }
        }
    }
}
