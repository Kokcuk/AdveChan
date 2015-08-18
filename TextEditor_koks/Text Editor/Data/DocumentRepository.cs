namespace TextEditor.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.OleDb;
    using Domain;

    public class DocumentRepository
    {
        private readonly string _connectionSettings;

        public DocumentRepository(string connectionString)
        {
            _connectionSettings = connectionString;
            //_connectionSettings="Provider=Microsoft.Jet.OLEDB.4.0; Data Source=D:\\everything\\GitHub\\Chan\\Text Editor\\DocumentsDB.mdb";
        }

        public IEnumerable<DocumentEntity> GetDocumentList()
        {
            using (OleDbConnection dbConnection = new OleDbConnection(_connectionSettings))
            {
                dbConnection.Open();
                List<DocumentEntity> documents = new List<DocumentEntity>();
                OleDbCommand command = dbConnection.CreateCommand();
                command.CommandText = "SELECT DocName, Doc FROM Documents;";
                OleDbDataReader reader = command.ExecuteReader();
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        var document = new DocumentEntity
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            CompressedContent = (byte[])reader.GetValue(2),
                        };
                        documents.Add(document);
                    }
                }
                dbConnection.Close();
                return documents;
            }
        }

        public DocumentEntity GetDocument(int id)
        {
            using (OleDbConnection dbConnection = new OleDbConnection(_connectionSettings))
            {
                dbConnection.Open();
                DocumentEntity document = null;
                OleDbCommand command = dbConnection.CreateCommand();
                command.CommandText = String.Format("SELECT DocName, Doc FROM Documents WHERE Id = {0};", id);
                OleDbDataReader reader = command.ExecuteReader();
                if (reader != null)
                {
                    reader.Read();
                    document = new DocumentEntity
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        CompressedContent = (byte[]) reader.GetValue(2),
                    };
                }
                dbConnection.Close();
                return document;
            }
        }

        public void Create(DocumentEntity document)
        {
            using (OleDbConnection dbConnection = new OleDbConnection(_connectionSettings))
            {
                dbConnection.Open();
                OleDbCommand command = dbConnection.CreateCommand();
                command.CommandText = "INSERT INTO Documents(DocName, Doc) VALUES(@Name, @Text)";
                command.Parameters.Add("@Name", OleDbType.Char).Value = document.Name;
                command.Parameters.Add("@Text", OleDbType.Binary).Value = document.CompressedContent;
                command.ExecuteNonQuery();
                dbConnection.Close();
            }
        }

        public void Update(DocumentEntity item)
        {
            using (OleDbConnection dbConnection = new OleDbConnection(_connectionSettings))
            {
                dbConnection.Open();
                OleDbCommand command = dbConnection.CreateCommand();
                command.CommandText = "UPDATE Documents SET DocName = @Name, Doc=@Text WHERE Id = @DocId";
                command.Parameters.Add("@Name", OleDbType.Char).Value = item.Name;
                command.Parameters.Add("@Text", OleDbType.Binary).Value = item.CompressedContent;
                command.Parameters.Add("@DocId", OleDbType.Integer).Value = item.Id;
                command.ExecuteNonQuery();
                dbConnection.Close();
            }
        }

        public void Delete(int id)
        {
            using (OleDbConnection dbConnection = new OleDbConnection(_connectionSettings))
            {
                dbConnection.Open();
                OleDbCommand command = dbConnection.CreateCommand();
                command.CommandText = String.Format("DELETE FROM Documents WHERE Id = {0}", id);
                command.ExecuteNonQuery();
                dbConnection.Close();
            }
        }

    }
}
