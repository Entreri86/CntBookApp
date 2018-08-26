using SQLite;

namespace CntBookApp.Models
{
    public class Contact
    {
        /// <summary>
        /// Id del contacto dentro de la aplicacion.
        /// Con anotaciones de SQLite para la Tabla de almacenaje.
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        /// <summary>
        /// Nombre del contacto, maximo tamaño del campo en tabla 255
        /// </summary>
        [MaxLength(255)]
        public string FirstName { get; set; }

        /// <summary>
        /// Apellidos, , maximo tamaño del campo en tabla 255
        /// </summary>
        [MaxLength(255)]
        public string LastName { get; set; }

        /// <summary>
        /// Telefono, maximo tamaño del campo en tabla 255
        /// </summary>
        [MaxLength(255)]
        public string Phone { get; set; }

        /// <summary>
        /// Email del contacto, maximo tamaño del campo en tabla 255
        /// </summary>
        [MaxLength(255)]
        public string Email { get; set; }

        /// <summary>
        /// Bool que identifica como bloqueado al contacto.
        /// </summary>
        public bool IsBlocked { get; set; }
    }
}
