using SQLite;

namespace CntBookApp.Persistence
{
    public interface ISQLiteDb
    {
        /// <summary>
        /// Metodo que se encarga de manera Singleton de devolver la conexion contra la BBDD.
        /// </summary>
        /// <returns>Conexion unica contra la BBDD.</returns>
        SQLiteAsyncConnection GetSQLiteAsyncConnection();
    }
}
