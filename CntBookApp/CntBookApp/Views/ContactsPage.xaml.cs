using CntBookApp.Persistence;
using CntBookApp.ViewsModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CntBookApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ContactsPage : ContentPage
	{
        //Modelo de la lista de contactos vinculado al BindingContext para el DataBinding.
        public ContactsPageViewModel ViewModel
        {
            get { return BindingContext as ContactsPageViewModel; }
            set { BindingContext = value; }
        }

        public ContactsPage ()
		{
            //Al iniciar el objeto creamos una instancia de la BBDD y otra del servicio de las Pages
            var contactStore = new SQLiteContactStore(DependencyService.Get<ISQLiteDb>());
            var pageService = new PageService();
            //Iniciamos el modelo de datos con la BBDD y el servicio de la Page
            ViewModel = new ContactsPageViewModel(contactStore, pageService);

            InitializeComponent ();
		}
        //Sobreescritura del metodo OnAppearing, antes de iniciar la vista cargamos los datos en la ListView (DataBinding)
        protected override void OnAppearing()
        {
            //Al abrir la pagina cargamos los datos en la Page desde la BBDD
            ViewModel.LoadDataCommand.Execute(null);

            base.OnAppearing();
        }
        //Metodo encargado de lanzar el comando del modelo para lanzar la Page.
        void OnContactSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //Ejecutamos el comando para lanzar la page de detalles de contacto
            ViewModel.SelectContactCommand.Execute(e.SelectedItem);
        }
    }
}