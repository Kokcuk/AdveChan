using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Text_Editor.Helpers;
using Text_Editor.Models;

namespace Text_Editor.ViewModel
{
    class EditorViewModel
    {
        private DocumentModel _model;
        private readonly IArchiver _archiver;
        private readonly IRepository<DocumentEntity> _repository; 

        public EditorViewModel()
        {
            _archiver = new Archiver();
            _repository = new AccessRepository();
        }

        public void CreateNew()
        {
            _model=new DocumentModel();
        }

        public void Save(string name, string text)
        {
            _model.Name = name;
            _model.Text = text;
            var entity = ConvertToEntity(_model);
            _repository.Update(entity);            
        }

        public void SaveAs(string name, string text)
        {
            _model.Name = name;
            _model.Text = text;
            var entity = ConvertToEntity(_model);
            _repository.Create(entity);
        }

        public DocumentModel Open(int id)
        {
            var entity = _repository.GetDocument(id);
            _model = ConvertToModel(entity);
            _model.IsOpened = true;
            return _model;
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        private DocumentEntity ConvertToEntity(DocumentModel model)
        {
            var entityToReturn = new DocumentEntity
            {
                Name = model.Name,
                Text = _archiver.Compress(model.Text)
            };
            return entityToReturn;
        }

        private DocumentModel ConvertToModel(DocumentEntity entity)
        {
            var modelToReturn = new DocumentModel
            {
                Id=entity.Id,
                IsOpened = true,
                Name = entity.Name,
                Text = _archiver.Decompress(entity.Text)
            };
            return modelToReturn;
        }

    }
}
