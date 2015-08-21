using System.Diagnostics.Eventing.Reader;

namespace TextEditor.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.OleDb;
    using Domain;

    // Here i create repository-layer to simplify work with database and support MVVM pattern

    public class DocumentRepository
    {
        private readonly string _connectionSettings;

        public DocumentRepository(string connectionString)
        {
            _connectionSettings = connectionString; //In this line we set connection settings(provider and path to DB)
        }

        public IEnumerable<DocumentEntity> GetDocumentList() //This method returns list of text documents
        {
            using (OleDbConnection dbConnection = new OleDbConnection(_connectionSettings))
            {
                dbConnection.Open();
                List<DocumentEntity> documents = new List<DocumentEntity>();
                OleDbCommand command = dbConnection.CreateCommand();
                command.CommandText = "SELECT Id, DocName, ChangeTime FROM Documents;";
                OleDbDataReader reader = command.ExecuteReader();
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        var document = new DocumentEntity
                        {
                            Id = (int) reader[0],
                            Name = reader[1].ToString(),
                        };
                        if (reader[2] != DBNull.Value) //Protection from null-data strings
                            document.TimeOfChanging = (DateTime) reader[2];
                        documents.Add(document);
                    }
                }
                dbConnection.Close();
                return documents;
            }
        }

        public DocumentEntity GetDocument(int id) //In this method we get text document by it's ID
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
                        Id = id,
                        Name = reader[0].ToString(),
                    };
                    if (reader[1] != DBNull.Value)
                        document.CompressedContent = (byte[]) reader[1]; //Protection from empty docs
                    else document.CompressedContent = null;
                }
                dbConnection.Close();
                return document;
            }
        }

        public void Create(DocumentEntity document)
            //In this method we create new record in database using DB's Table entity as parametr
        {
            using (OleDbConnection dbConnection = new OleDbConnection(_connectionSettings))
            {
                dbConnection.Open();
                OleDbCommand command = dbConnection.CreateCommand();
                command.CommandText = "INSERT INTO Documents(DocName, Doc, ChangeTime) VALUES(@Name, @Text, Now())";
                command.Parameters.Add("@Name", OleDbType.Char).Value = document.Name;
                command.Parameters.Add("@Text", OleDbType.Binary).Value = document.CompressedContent;
                command.ExecuteNonQuery();
                dbConnection.Close();
            }
        }

        public void Update(DocumentEntity item) //In this method we updating record in DB
        {
            using (OleDbConnection dbConnection = new OleDbConnection(_connectionSettings))
            {
                dbConnection.Open();
                OleDbCommand command = dbConnection.CreateCommand();
                command.CommandText =
                    "UPDATE Documents SET DocName = @Name, Doc=@Text, ChangeTime=Now() WHERE Id = @DocId";
                command.Parameters.Add("@Name", OleDbType.Char).Value = item.Name;
                command.Parameters.Add("@Text", OleDbType.Binary).Value = item.CompressedContent;
                command.Parameters.Add("@DocId", OleDbType.Integer).Value = item.Id;
                command.ExecuteNonQuery();
                dbConnection.Close();
            }
        }

        public void Delete(int id) //In this method we delete record from DB
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