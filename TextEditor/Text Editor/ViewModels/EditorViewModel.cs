using System.Threading.Tasks;
using TextEditor.Views;

//This is a main class, which contains almost all logic of Text-Editor 

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
        private string _content;

        public RelayCommand OpenCommand { get; set; } //Theese commands' purpose to bind class methods on buttons 
        public RelayCommand SaveCommand { get; set; } //on View(due to MVVM pattern)
        public RelayCommand CreateCommand { get; set; }
        public RelayCommand SaveAsCommand { get; set; }

        public string Content //This field is binded on RichTextBox's text 
        {
            get { return _content; }
            set
            {
                _content = value;
                RaisePropertyChanged("Content");
            }
        }

        public EditorViewModel()
        {
            _stringCompressor = new StringCompressor();
            var connectionString = ConfigurationManager.ConnectionStrings["database"].ConnectionString;
                //Here we take database settings from App-config as it was said in the task
            _documentRepository = new DocumentRepository(connectionString);
            _documentModel = new DocumentModel(); //Here we create object of model

            OpenCommand = new RelayCommand(x => Open());
            CreateCommand = new RelayCommand(x => Create());
            SaveCommand = new RelayCommand(x => Save(), x => CheckIfOpened());
            SaveAsCommand = new RelayCommand(x => SaveAs());
        }

        private void Create() //Method that creates empty document
        {
            _documentModel = new DocumentModel();
            Content = _documentModel.Text;
        }

        private async void Open() //Method that oppens list of docs in a new view(as it was said in the task)
        {
            var openView = new OpenView();
            var viewModel = new OpenViewModel();
            openView.DataContext = viewModel;
            openView.Show(); //Here we call view wich contains list of documents in the DB
            await Task.Factory.StartNew(() => //Here i satisfied additional condition(open document in a new thread)
            {
                openView.Closed += (e, o) =>
                {
                    if (viewModel.SelectedDocument.Id != 0)
                    {
                        DocumentModel model =
                            ConvertToModel(_documentRepository.GetDocument(viewModel.SelectedDocument.Id));
                        _documentModel = model;
                        _documentModel.IsOpened = true;
                        Content = model.Text;
                    }
                };
            });
        }

        private void Save() //This method saves opened file without creating new file
        {
            if (_documentModel.IsOpened)
            {
                var entity = ConvertToEntity(_documentModel);
                _documentRepository.Update(entity);
                MessageBox.Show("Document has been saved");
            }
        }

        private bool CheckIfOpened()
            //This method's appointment is to disable "Save" button in case that document wasnt opened
        {
            if (_documentModel.IsOpened)
                return true;
            return false;
        }

        private void SaveAs() //This method makes a new record in DB with document
        {
            var saveView = new SaveView();
            var viewModel = new SaveViewModel();
            saveView.DataContext = viewModel;
            saveView.Show(); //Here we call SaveView to enter the document's name
            saveView.Closed += (e, o) =>
            {
                if (!string.IsNullOrEmpty(viewModel.DocumentName))
                {
                    _documentModel.Name = viewModel.DocumentName;
                    _documentModel.Text = Content;
                    var entity = ConvertToEntity(_documentModel);
                    _documentRepository.Create(entity);
                    MessageBox.Show("Document has been saved with name " + viewModel.DocumentName);
                }
            };
        }

        private DocumentEntity ConvertToEntity(DocumentModel model) //This method convert MVVM model to DataBase entity
        {
            var entity = new DocumentEntity
            {
                CompressedContent = _stringCompressor.Compress(model.Text),
                Id = model.Id,
                Name = model.Name
            };
            return entity;
        }

        private DocumentModel ConvertToModel(DocumentEntity entity) //This method converts DB entity to MVVM model
        {
            var documentModel = new DocumentModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Text = _stringCompressor.Decompress(entity.CompressedContent)
            };
            return documentModel;
        }
    }
}