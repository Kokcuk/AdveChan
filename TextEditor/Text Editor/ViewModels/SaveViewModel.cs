using System.Linq;
using System.Windows;
using System.Windows.Input;
using TextEditor.Views;

namespace TextEditor.ViewModels
{
    //This is a ViewModel of save view
    public class SaveViewModel : ViewModelBase
    {
        public string DocumentName { get; set; }
        public RelayCommand ConfirmCommand { get; set; }

        public SaveViewModel()
        {
            ConfirmCommand = new RelayCommand(x => CloseWindow());
        }

        private void CloseWindow()
        {
            var window = (Window) Application.Current.Windows.OfType<SaveView>().FirstOrDefault();
            window.Close();
        }
    }
}