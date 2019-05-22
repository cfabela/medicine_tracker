using System;

using SQLite;


//Interface definition in common Xamarin Code
namespace MedicineTracker.Database
{
    public interface IDBInterface
    {
        SQLiteConnection CreateConnection();
    }
}
