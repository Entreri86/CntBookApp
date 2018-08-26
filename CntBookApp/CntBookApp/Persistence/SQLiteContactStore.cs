using CntBookApp.Models;
using CntBookApp.ViewsModels;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CntBookApp.Persistence
{
    public class SQLiteContactStore : IContactStore
    {
        //Implementamos la interfaz asi como todos sus metodos.
        //Propiedad privada para recoger la conexion y realizar la actualizacion de los valores de la lista de contactos.
        private SQLiteAsyncConnection _connection;

        /// <summary>
        /// Constructor de la clase, inicializa la BBDD y se encarga de crear las tablas y-o inicilizarlas.
        /// </summary>
        /// <param name="db">Instancia de la BBDD a inicializar.</param>
        public SQLiteContactStore(ISQLiteDb db)
        {
            try
            {
                //recogemos conexion
                _connection = db.GetSQLiteAsyncConnection();
                //Creamos las tablas (si estan creadas las inicia)
                _connection.CreateTableAsync<Contact>();
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Metodo implementado de la interfaz, que añade el contacto dado por parametro en la BBDD.
        /// </summary>
        /// <param name="contact">Contacto a añadir.</param>
        /// <returns>Tarea en un hilo a parte</returns>
        public async Task AddContact(Contact contact) => await _connection.InsertAsync(contact).ConfigureAwait(false);

        /// <summary>
        /// Metodo implementado de la interfaz, que elimina el contacto ya existente en la BBDD.
        /// </summary>
        /// <param name="contact">Contacto a eliminar.</param>
        /// <returns>Tarea en un hilo a parte</returns>
        public async Task DeleteContact(Contact contact) => await _connection.DeleteAsync(contact).ConfigureAwait(false);

        /// <summary>
        /// Metodo implementado de la interfaz, que segun el Id dado por parametro retorna el contacto de la BBDD.
        /// </summary>
        /// <param name="id">Id del contacto que retornar.</param>
        /// <returns>Tarea en un hilo a parte</returns>
        public async Task<Contact> GetContact(int id) => await _connection.FindAsync<Contact>(id).ConfigureAwait(false);

        /// <summary>
        /// Metodo implementado de la interfaz, que se encarga de recoger los contactos de la BBDD de SQLite de manera asincrona.
        /// </summary>
        /// <returns>Lista de los contactos existentes en la BBDD.</returns>
        public async Task<IEnumerable<Contact>> GetContactsAsync() => await _connection.Table<Contact>().ToListAsync().ConfigureAwait(false);

        /// <summary>
        /// Metodo implementado de la interfaz, que elimina el contacto ya existente en la BBDD de manera asincrona.
        /// </summary>
        /// <param name="contact">Contacto a reemplazar en la BBDD.</param>
        /// <returns>Tarea en un hilo a parte</returns>
        public async Task UpdateContact(Contact contact) => await _connection.UpdateAsync(contact).ConfigureAwait(false);
        
    }
}
