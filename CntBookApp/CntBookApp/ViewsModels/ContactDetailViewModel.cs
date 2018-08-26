using CntBookApp.Models;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CntBookApp.ViewsModels
{
    public class ContactDetailViewModel
    {
        private readonly IContactStore _contactStore;
        private readonly IPageService _pageService;
              
        public Contact Contact { get; private set; }

        /// <summary>
        /// Comando de guardado del boton.
        /// </summary>
        public ICommand SaveCommand { get; private set; }

        /// <summary>
        /// Constructor de la clase que se encarga de inicializar el almacenamiento en la BBDD y los servicios de la Page
        /// </summary>
        /// <param name="viewModel">Modelo con el que se comunica la vista.</param>
        /// <param name="contactStore">Controlador donde se alojan los datos.</param>
        /// <param name="pageService">Servicios de la Page de Xamarin.</param>
        public ContactDetailViewModel(ContactViewModel viewModel, IContactStore contactStore, IPageService pageService)
        {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            _pageService = pageService;
            _contactStore = contactStore;
            //Inicializamos el comando del boton de guardar.
            SaveCommand = new Command(async () => await Save());

            // Here we are mapping our view model to a domain model. 
            // This is required because further below where we use ContactStore 
            // to add or update a contact, we need a domain model, not a view model.
            // Read my comment in the constructor of ContactViewModel.

            Contact = new Contact
            {
                Id = viewModel.Id,
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                Phone = viewModel.Phone,
                Email = viewModel.Email,
                IsBlocked = viewModel.IsBlocked,
            };
        }

        /// <summary>
        /// Metodo encargado de guardar en la BBDD el contacto al pulsar el boton de Save.
        /// </summary>
        /// <returns>Tarea en un hilo a parte.</returns>
        async Task Save()
        {
            //Si el nombre o el apellido estan vacios mostramos alerta al usuario.
            if (String.IsNullOrWhiteSpace(Contact.FirstName) &&
                String.IsNullOrWhiteSpace(Contact.LastName))
            {
                await _pageService.DisplayAlert("Error", "Please enter the name.", "OK");
                return;
            }

            if (Contact.Id == 0)//Si el contacto es nuevo (no esta actualizando uno existente)
            {
                //añadimos a la lista
                await _contactStore.AddContact(Contact);
                //Si el contacto ha sido añadido lanzamos evento mediante el centro de mensajes
                MessagingCenter.Send(this, Events.ContactAdded, Contact);
            }
            else
            {//Si no esta vacio el Id del contacto estan actualizando uno existente.
                await _contactStore.UpdateContact(Contact);
                //Si se ha añadido a la lista disparamos evento mediante el centro de mensajes
                MessagingCenter.Send(this, Events.ContactUpdated, Contact);
            }
            //Y volvemos a la lista de contactos mediante el servicio de la PAge.
            await _pageService.PopAsync();
        }
    }
}
