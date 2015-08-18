namespace TextEditor.ViewModels
{
    using System.Configuration;
    using System.Windows;
    using TextEditor.Data;
    using TextEditor.Domain;
    using TextEditor.StringCompressing;
    using Text_Editor.Models;

    public class EditorViewModel : ViewModelBase
    {
        private readonly IStringCompressor _stringCompressor;
        private readonly DocumentRepository _documentRepository;
        private DocumentModel _documentModel;

        public RelayCommand OpenCommand { get; set; }
        public RelayCommand SaveCommand { get; set; }
        public RelayCommand CreateCommand { get; set; }

        public EditorViewModel()
        {
            _stringCompressor = new StringCompressor();

            var connectionString = ConfigurationManager.ConnectionStrings["database"].ConnectionString;
            _documentRepository = new DocumentRepository(connectionString);
            _documentModel = new DocumentModel();

            OpenCommand = new RelayCommand(x=>Open());
            CreateCommand = new RelayCommand(x => Create());
        }

        private void Create()
        {
            MessageBox.Show("Suka");
        }

        private void Open()
        {
            MessageBox.Show("Suka");
        }

        private string _content;

        public string Content
        {
            get { return _content; }
            set { _content = value; RaisePropertyChanged("Content"); }
        }
    }
}
