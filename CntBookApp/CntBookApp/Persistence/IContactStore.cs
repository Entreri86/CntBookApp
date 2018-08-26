using CntBookApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CntBookApp.ViewsModels
{
    public interface IContactStore
    {
        /// <summary>
        /// Metodo que se encarga de recoger los contactos de la BBDD de SQLite de manera asincrona.
        /// </summary>
        /// <returns>Lista de los contactos existentes en la BBDD en un hilo a parte</returns>
        Task<IEnumerable<Contact>> GetContactsAsync();

        /// <summary>
        /// Metodo que segun el Id dado por parametro retorna el contacto de la BBDD de manera asincrona.
        /// </summary>
        /// <param name="id">Id del contacto que retornar.</param>
        /// <returns>Tarea en un hilo a parte</returns>
        Task<Contact> GetContact(int id);

        /// <summary>
        /// Metodo que añade el contacto dado por parametro en la BBDD de manera asincrona.
        /// </summary>
        /// <param name="contact">Contacto a añadir.</param>
        /// <returns>Tarea en un hilo a parte</returns>
        Task AddContact(Contact contact);

        /// <summary>
        /// Metodo que actualiza el contacto ya existente en la BBDD de manera asincrona.
        /// </summary>
        /// <param name="contact">Contacto a reemplazar en la BBDD.</param>
        /// <returns>Tarea en un hilo a parte</returns>
        Task UpdateContact(Contact contact);

        /// <summary>
        /// Metodo que elimina el contacto ya existente en la BBDD de manera asincrona.
        /// </summary>
        /// <param name="contact">Contacto a eliminar.</param>
        /// <returns>Tarea en un hilo a parte</returns>
        Task DeleteContact(Contact contact);
    }
}
