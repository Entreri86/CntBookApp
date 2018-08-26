using CntBookApp.Persistence;
using CntBookApp.UWP;
using SQLite;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLiteDb))]
namespace CntBookApp.UWP
{
    public class SQLiteDb : ISQLiteDb
    {
        public SQLiteAsyncConnection GetSQLiteAsyncConnection()
        {
            var path = Windows.Storage.ApplicationData.Current.LocalFolder.Path;
            string fullPath = Path.Combine(path, "MySQLite.db3");

            return new SQLiteAsyncConnection(fullPath);
        }
    }
}
