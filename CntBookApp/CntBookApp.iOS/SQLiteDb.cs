using System;
using System.IO;
using CntBookApp.iOS;
using CntBookApp.Persistence;
using SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLiteDb))]
namespace CntBookApp.iOS
{
    public class SQLiteDb : ISQLiteDb
    {
        public SQLiteAsyncConnection GetSQLiteAsyncConnection()
        {
            var documentsPath = Path.Combine(Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments));
                        
            var path = Path.Combine(documentsPath, "MySQLite.db3");

            return new SQLiteAsyncConnection(path);

        }
    }
}