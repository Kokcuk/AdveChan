using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//According to MVVM pattern this is a model

namespace Text_Editor.Models
{
    internal class DocumentModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public bool IsOpened { get; set; }
    }
}