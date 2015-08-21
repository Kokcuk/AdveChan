using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Windows;
using TextEditor.Data;
using TextEditor.Domain;
using TextEditor.Views;

namespace TextEditor.ViewModels
{
    public class OpenViewModel : ViewModelBase
    {
        private DocumentRepository _documentRepository;
        private ObservableCollection<DocumentEntity> _documents; //With this collection we fill ListView in View
        private DocumentEntity _selectedDocument; //Selected document

        public ObservableCollection<DocumentEntity> Documents
        {
            get { return _documents; }
            set
            {
                _documents = value;
                RaisePropertyChanged("Documents");
            }
        }

        public DocumentEntity SelectedDocument
        {
            get { return _selectedDocument; }
            set
            {
                _selectedDocument = value;
                RaisePropertyChanged("SelectedOne");
            }
        }

        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand OpenCommand { get; set; }

        public OpenViewModel()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["database"].ConnectionString;
            _documentRepository = new DocumentRepository(connectionString);
            Documents = new ObservableCollection<DocumentEntity>(_documentRepository.GetDocumentList());
            DeleteCommand = new RelayCommand(x => Delete());
            OpenCommand = new RelayCommand(x => Open());
        }

        private void Open()
        {
            var window = (Window) Application.Current.Windows.OfType<OpenView>().FirstOrDefault();
            window.Close();
        }

        private void Delete()
        {
            _documentRepository.Delete(SelectedDocument.Id);
        }
    }
}