using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Xceed.Wpf.Toolkit;

namespace TextEditor.Views
{
    //This class contains all decoration methods for RichTextBox's text
    public partial class EditorView : Window
    {
        public EditorView()
        {
            InitializeComponent();
            cmbFontSize.ItemsSource = new List<double>() {2, 4, 6, 8, 10, 12, 14, 18, 22, 26, 30};
        }

        private void cbBoldFont_CheckedChanged(object sender, RoutedEventArgs e)
        {
            if (cbBoldFont.IsChecked == true)
            {
                richTextBox.Selection.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);
            }
            else
            {
                richTextBox.Selection.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Normal);
            }
        }

        private void cbItalicFont_CheckedChanged(object sender, RoutedEventArgs e)
        {
            if (cbItalicFont.IsChecked == true)
            {
                richTextBox.Selection.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Italic);
            }
            else
            {
                richTextBox.Selection.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Normal);
            }
        }

        private void cbUnderlineFont_CheckedChanged(object sender, RoutedEventArgs e)
        {
            if (cbUnderlineFont.IsChecked == true)
            {
                richTextBox.Selection.ApplyPropertyValue(TextBlock.TextDecorationsProperty, TextDecorations.Underline);
            }
            else
            {
                richTextBox.Selection.ApplyPropertyValue(TextBlock.TextDecorationsProperty, null);
            }
        }

        private void RichTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (cbBoldFont.IsChecked == true)
            {
                richTextBox.Selection.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);
            }
            if (cbBoldFont.IsChecked == false)
            {
                richTextBox.Selection.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Normal);
            }
            if (cbItalicFont.IsChecked == true)
            {
                richTextBox.Selection.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Italic);
            }
            if (cbItalicFont.IsChecked == false)
            {
                richTextBox.Selection.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Normal);
            }
            if (cbUnderlineFont.IsChecked == true)
            {
                richTextBox.Selection.ApplyPropertyValue(TextBlock.TextDecorationsProperty, TextDecorations.Underline);
            }
            if (cbUnderlineFont.IsChecked == false)
            {
                richTextBox.Selection.ApplyPropertyValue(TextBlock.TextDecorationsProperty, null);
            }
            richTextBox.Selection.ApplyPropertyValue(TextElement.FontSizeProperty, cmbFontSize.SelectedValue);
        }

        private void CmbFontSize_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            richTextBox.Selection.ApplyPropertyValue(TextElement.FontSizeProperty, cmbFontSize.SelectedValue);
        }
    }
}