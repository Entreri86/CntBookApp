using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace CntBookApp.ViewsModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Metodo que al cambiar la propiedad dispara un evento hacia la vista para notificar de los cambios y visualizar el ultimo valor.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad al que disparar el evento.</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        /// <summary>
        /// Metodo generico que compara dos objetos si el valor es diferente al almacenado en el campo backingfield lo asigna y notifica a la vista.
        /// </summary>
        /// <typeparam name="T">Objeto generico a asignar el valor.</typeparam>
        /// <param name="backingField">Campo con anterior valor.</param>
        /// <param name="value">Campo con el valor actual a comparar</param>
        /// <param name="propertyName">Parametro con el valor del nombre de la propiedad.</param>
        protected void SetValue<T>(ref T backingField, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingField, value)) return;
            //reasignamos si son diferentes
            backingField = value;
            //notificamos a la vista
            OnPropertyChanged(propertyName);
        }
    }
}
