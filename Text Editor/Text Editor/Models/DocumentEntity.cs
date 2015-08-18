using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Text_Editor.Models
{
    //Here i create database's table entity
    class DocumentEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Text { get; set; }
    }
}
