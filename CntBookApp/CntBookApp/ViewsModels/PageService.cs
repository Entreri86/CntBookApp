using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CntBookApp.ViewsModels
{
    public class PageService : IPageService
    {
        //Implementamos la interfaz para personalizar las alertas y no depender de Xamarin para Unit Test

        /// <summary>
        /// Propiedad privada para conocer la pagina principal y lanzar la alerta mediante el get.
        /// </summary>
        private Page MainPage
        {
            get { return Application.Current.MainPage; }
        }

        /// <summary>
        /// Metodo encargado de mostrar una alerta al usuario con un mensaje personalizado esperando la respuesta del mismo.
        /// </summary>
        /// <param name="title">Titulo del mensaje</param>
        /// <param name="message">Mensaje a mostrar al usuario.</param>
        /// <param name="ok">Nombre de primer boton.</param>
        /// <param name="cancel">Nombre del segundo boton.</param>
        /// <returns>Tarea en hilo a parte y bool con el resultado del usuario.</returns>
        public async Task<bool> DisplayAlert(string title, string message, string ok, string cancel) => await MainPage.DisplayAlert(title, message, ok, cancel).ConfigureAwait(false);

        /// <summary>
        /// /// Metodo encargado de mostrar una alerta al usuario con un mensaje personalizado sin esperar respuesta de el.
        /// </summary>
        /// <param name="title">Titulo del mensaje</param>
        /// <param name="message">Mensaje a mostrar al usuario.</param>
        /// <param name="ok">Nombre de primer boton.</param>
        /// <returns>>Tarea en hilo a parte.</returns>
        public async Task DisplayAlert(string title, string message, string ok) => await MainPage.DisplayAlert(title, message, ok).ConfigureAwait(false);

        /// <summary>
        /// Metodo de Navegacion hacia atras  en direccion a la anterior Page abierta.
        /// </summary>
        /// <returns>Tarea en hilo a parte.</returns>
        public async Task<Page> PopAsync() => await MainPage.Navigation.PopAsync().ConfigureAwait(false);

        /// <summary>
        /// Metodo de Navegacion hacia delante la Page siguiente.
        /// </summary>
        /// <param name="page">Page a la que navegar siguiente.</param>
        /// <returns>Tarea en hilo a parte.</returns>
        public async Task PushAsync(Page page) => await MainPage.Navigation.PushAsync(page).ConfigureAwait(false);
    }
}
