using CntBookApp.Persistence;
using CntBookApp.ViewsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CntBookApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ContactDetailPage : ContentPage
	{
		public ContactDetailPage (ContactViewModel viewModel)
		{
			InitializeComponent ();
            //Iniciamos el almacenamiento en la BBDD desde aqui en el detalle de la Page de contactos.
            var contactStore = new SQLiteContactStore(DependencyService.Get<ISQLiteDb>());
            //Iniciamos el servicio de Xamarin.
            var pageService = new PageService();
            //Asignamos el contexto al modelo detallado de la vista para el DataBinding, en caso de que no este iniciado,
            //lo iniciamos con los parametros.
            BindingContext = new ContactDetailViewModel(viewModel ?? new ContactViewModel(),contactStore, pageService);
		}
	}
}