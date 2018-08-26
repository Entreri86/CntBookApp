using CntBookApp.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CntBookApp.ViewsModels
{
    public class ContactsPageViewModel : BaseViewModel
    {
        //Clase que hereda de BaseViewModel para comunicarse con el modelo mediante notificaciones.
        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public ContactsPageViewModel() { }
        //Campos privados (backingField) para no exponer a la vista los valores directamente.
        private ContactViewModel _selectedContact;
        private IContactStore _contactStore;
        private IPageService _pageService;
        private bool _isDataLoaded;
        //Comandos (acciones que se desencadenan desde botones de la vista) que actuan como Eventos de la Vista.
        public ICommand LoadDataCommand { get; private set; }
        public ICommand AddContactCommand { get; private set; }
        public ICommand SelectContactCommand { get; private set; }
        public ICommand DeleteContactCommand { get; private set; }

        /// <summary>
        /// Inicializamos Contactos a una nueva ObservableCollection aunque despues lo configuremos mas tarde al cargar los contactos.
        /// Si no lo hacemos como esta en ContactsPage enlazado el ItemSource de la ListView a esta propiedad si no la inicializamos nos
        /// saltara un NullPointerException.
        /// </summary>
        public ObservableCollection<ContactViewModel> Contacts { get; private set; }
            = new ObservableCollection<ContactViewModel>();

        /// <summary>
        /// Campo publico para ser accesible por DataBinding desde la Vista.
        /// </summary>
        public ContactViewModel SelectedContact
        {
            get { return _selectedContact; }
            set { SetValue(ref _selectedContact, value); }
        }

        /// <summary>
        /// Constructor de  la clase, inicializa el almacenamiento en la BBDD asi como el servicio de Page Xamarin.
        /// </summary>
        /// <param name="contactStore">Instancia de la BBDD donde guardar los contactos.</param>
        /// <param name="pageService">Servicio de las Page con acceso a Xamarin.</param>
        public ContactsPageViewModel(IContactStore contactStore, IPageService pageService)
        {
            _contactStore = contactStore;
            _pageService = pageService;
            //Como LoadData y los demas metodos es async y tiene tipo de retorno Task no podemos pasarle el nombre del metodo como Action al constructor 
            //de la clase Command (Xamarin.Forms)tenemos que crear una Inline Action con Lambda y llamarlo manualmente con el Await.            
            LoadDataCommand = new Command(async () => await LoadData().ConfigureAwait(false));
            AddContactCommand = new Command(async () => await AddContact().ConfigureAwait(false));
            SelectContactCommand = new Command<ContactViewModel>(async c => await SelectContact(c).ConfigureAwait(false));
            DeleteContactCommand = new Command<ContactViewModel>(async c => await DeleteContact(c).ConfigureAwait(false));

            //Subscribimos las propiedades al modelo de la Vista de la Page para eliminar los eventos.
            MessagingCenter.Subscribe<ContactDetailViewModel, Contact>(this, Events.ContactAdded, OnContactAdded);
            MessagingCenter.Subscribe<ContactDetailViewModel, Contact>(this, Events.ContactUpdated, OnContactUpdated);
        }

        /// <summary>
        /// Metodo que comprueba si los datos estan cargados, en el caso de no estarlos hace una consulta a SQLite Db y asigna a la lista todos los 
        /// contactos de la BBDD local.
        /// </summary>
        /// <returns>Tarea en hilo a parte.</returns>
        private async Task LoadData()
        {
            if (_isDataLoaded)
                return;

            _isDataLoaded = true;
            //Recogemos todos los contactos de la tabla de la BD 
            var contacts = await _contactStore.GetContactsAsync();
            //Por cada uno de ellos definimos un objeto nuevo en la Vista.
            foreach (var c in contacts)
                Contacts.Add(new ContactViewModel(c));
        }

        private void OnContactAdded(ContactDetailViewModel source, Contact contact)
        {
            Contacts.Add(new ContactViewModel(contact));
        }

        /// <summary>
        /// Metodo que se encarga mediante MessagingCenter de lanzar un evento hacia la clase de la propiedad, para que esta recoga el valor actualizado.
        /// </summary>
        /// <param name="source">Origen del cambio de valor.</param>
        /// <param name="contact">Contacto a actualizar.</param>
        private void OnContactUpdated(ContactDetailViewModel source, Contact contact)
        {
            //Buscamos en la coleccion el contacto primero antes de actualizarlo. 
            var contactInList = Contacts.Single(c => c.Id == contact.Id);

            contactInList.Id = contact.Id;
            contactInList.FirstName = contact.FirstName;
            contactInList.LastName = contact.LastName;
            contactInList.Phone = contact.Phone;
            contactInList.Email = contact.Email;
            contactInList.IsBlocked = contact.IsBlocked;
        }

        /// <summary>
        /// Metodo encargado de añadir un nuevo contacto a la lista y lanzar la Page de detalle de contactos para ser rellenada por el usuario.
        /// </summary>
        /// <returns>Tarea en hilo a parte.</returns>
        private async Task AddContact() => await _pageService.PushAsync(new ContactDetailPage(new ContactViewModel()));

        /// <summary>
        /// Metodo encargado de seleccionar un contacto y lanzar la pagina de detalles. 
        /// </summary>
        /// <param name="contact">Contacto seleccionado por el usuario.</param>
        /// <returns>Tarea en hilo a parte.</returns>
        private async Task SelectContact(ContactViewModel contact)
        {
            //Si el contacto es null retornamos.
            if (contact == null)
                return;
            //Reseteamos la seleccion de la vista (quitar el foco)
            SelectedContact = null;
            await _pageService.PushAsync(new ContactDetailPage(contact));
        }

        /// <summary>
        /// Metodo encargado de eliminar un contacto, mostrando alerta al usuario mediante el servicio de pagina.
        /// </summary>
        /// <param name="contactViewModel">Contacto a eliminar de la BBDD</param>
        /// <returns>Tarea en hilo a parte.</returns>
        private async Task DeleteContact(ContactViewModel contactViewModel)
        {
            var a = "";
            //Si el usuario confirma la eliminacion del contacto
            if (await _pageService.DisplayAlert("Warning", $"Are you sure you want to delete {contactViewModel.FullName}?", "Yes", "No"))
            {
                //Lo eliminamos de la listView
                Contacts.Remove(contactViewModel);

                // Creamos un contacto y lo recogemos de la BBDD para ahorrar memoria para no tenerlo constantemente inicializado,
                // pues si crece mucho la lista de contactos podria desperdiciarse memoria, despues lanzamos la consulta para eliminar de la BBDD el contacto                
                var contact = await _contactStore.GetContact(contactViewModel.Id);
                await _contactStore.DeleteContact(contact);
            }
        }
    }
}
