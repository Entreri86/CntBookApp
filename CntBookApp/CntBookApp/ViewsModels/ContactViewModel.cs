using CntBookApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CntBookApp.ViewsModels
{
    public class ContactViewModel : BaseViewModel
    {
        /// <summary>
        /// Constructor de la clase que hereda de BaseViewModel para comunicarse con el modelo mediante notificaciones,
        /// inicializamos los atributos al iniciar el modelo.
        /// </summary>
        /// <param name="contact">Contacto que gestionar mediante el modelo.</param>
        public ContactViewModel(Contact contact)
        {
            Id = contact.Id;
            _firstName = contact.FirstName;
            _lastName = contact.LastName;
            Phone = contact.Phone;
            Email = contact.Email;
            IsBlocked = contact.IsBlocked;
        }
        //Constructor por defecto.
        public ContactViewModel() { }
        //Atributos publicos para enlazar con la vista
        public int Id { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool IsBlocked { get; set; }

        /// <summary>
        /// Campo privado (backingField) de soporte, asi no exponemos el campo real al hacer el Set. 
        /// </summary>
        private string _firstName;

        /// <summary>
        /// Campo publico para ser accesible por DataBinding desde la Vista.
        /// </summary>
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                SetValue(ref _firstName, value);
                OnPropertyChanged(nameof(FullName));
            }
        }

        /// <summary>
        /// Campo privado (backingField) de soporte, asi no exponemos el campo real al hacer el Set. 
        /// </summary>
        private string _lastName;

        /// <summary>
        /// Campo publico para ser accesible por DataBinding desde la Vista.
        /// </summary>
        public string LastName
        {
            get { return _lastName; }
            set
            {
                SetValue(ref _lastName, value);
                OnPropertyChanged(nameof(FullName));
            }
        }

        /// <summary>
        /// Atributo completo que retorna nombre + apellido a la Vista.
        /// </summary>
        public string FullName
        {
            get { return $"{FirstName} {LastName}"; }
        }
    }
}
