using System.Configuration;
using System.Data;
using System.Runtime.CompilerServices;
using System.Windows;

namespace WPF_ExpensesTracker
{
    public partial class App : Application
    {
        public App() 
        {
            this.Dispatcher.UnhandledException += Dispatcher_UnhandledException;
        }

        private void Dispatcher_UnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            e.Handled = true;
        }
    }

}
