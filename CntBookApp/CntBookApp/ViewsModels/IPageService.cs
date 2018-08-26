using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CntBookApp.ViewsModels
{
    public interface IPageService
    {
        //Interfaz para desligar la Vista de Xamarin (Unit Test)

        /// <summary>
        /// Metodo de Navegacion hacia delante la Page siguiente.
        /// </summary>
        /// <param name="page">Page a la que navegar siguiente.</param>
        /// <returns>Tarea en hilo a parte.</returns>
        Task PushAsync(Page page);

        /// <summary>
        /// Metodo de Navegacion hacia atras  en direccion a la anterior Page abierta.
        /// </summary>
        /// <returns>Tarea en hilo a parte.</returns>
        Task<Page> PopAsync();

        /// <summary>
        /// Metodo encargado de mostrar una alerta al usuario con un mensaje personalizado esperando la respuesta del mismo.
        /// </summary>
        /// <param name="title">Titulo del mensaje</param>
        /// <param name="message">Mensaje a mostrar al usuario.</param>
        /// <param name="ok">Nombre de primer boton.</param>
        /// <param name="cancel">Nombre del segundo boton.</param>
        /// <returns>Tarea en hilo a parte y bool con el resultado del usuario.</returns>
        Task<bool> DisplayAlert(string title, string message, string ok, string cancel);

        /// <summary>
        /// /// Metodo encargado de mostrar una alerta al usuario con un mensaje personalizado sin esperar respuesta de el.
        /// </summary>
        /// <param name="title">Titulo del mensaje</param>
        /// <param name="message">Mensaje a mostrar al usuario.</param>
        /// <param name="ok">Nombre de primer boton.</param>
        /// <returns>>Tarea en hilo a parte.</returns>
        Task DisplayAlert(string title, string message, string ok);
    }
}
