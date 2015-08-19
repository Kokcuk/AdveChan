namespace TextEditor
{
    using System.Windows;
    using ViewModels;
    using Views;

    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var editorView = new EditorView();
            editorView.Show();
            editorView.DataContext = new EditorViewModel();
        }
    }
}