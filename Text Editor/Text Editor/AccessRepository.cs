using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Text_Editor.Models;

namespace Text_Editor
{
    class AccessRepository:IRepository<DocumentEntity>
    {
        private string _connectionSettings;
        public AccessRepository()
        {
            _connectionSettings="Provider=Microsoft.Jet.OLEDB.4.0; Data Source=D:\\everything\\GitHub\\Chan\\Text Editor\\DocumentsDB.mdb";
        }

        public IEnumerable<DocumentEntity> GetDocumentList()
        {
            throw new NotImplementedException();
        }

        public DocumentEntity GetDocument(int id)
        {
            using (OleDbConnection dbConnection = new OleDbConnection(_connectionSettings))
            {
                dbConnection.Open();
                DocumentEntity entityToReturn = new DocumentEntity();
                OleDbCommand query = new OleDbCommand();
                query.Connection = dbConnection;
                query.CommandText = String.Format("SELECT DocName, Doc FROM Documents WHERE Id = {0};", id);
                OleDbDataReader reader = query.ExecuteReader();
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        entityToReturn = new DocumentEntity
                        {
                            Id = id,
                            Name = reader[0].ToString(),
                            Text = (byte[]) reader[1]
                        };
                    }
                }
                query.Connection.Close();
                return entityToReturn;
            }
        }

        public void Create(DocumentEntity item)
        {
            using (OleDbConnection dbConnection = new OleDbConnection(_connectionSettings))
            {
                dbConnection.Open();
                OleDbCommand query = new OleDbCommand();
                query.Connection = dbConnection;
                query.CommandText = "INSERT INTO Documents(DocName, Doc) VALUES(@Name, @Text)";
                query.Parameters.Add("@Name", OleDbType.Char).Value = item.Name;
                query.Parameters.Add("@Text", OleDbType.Binary).Value = item.Text;
                query.ExecuteNonQuery();
                query.Connection.Close();
            }
        }

        public void Update(DocumentEntity item)
        {
            using (OleDbConnection dbConnection = new OleDbConnection(_connectionSettings))
            {
                dbConnection.Open();
                OleDbCommand query  = new OleDbCommand();
                query.Connection = dbConnection;
                query.CommandText="UPDATE Documents SET DocName = @Name, Doc=@Text WHERE Id = @DocId";
                query.Parameters.Add("@Name", OleDbType.Char).Value = item.Name;
                query.Parameters.Add("@Text", OleDbType.Binary).Value = item.Text;
                query.Parameters.Add("@DocId", OleDbType.Integer).Value = item.Id;
                query.ExecuteNonQuery();
                query.Connection.Close();
            }
        }

        public void Delete(int id)
        {
            using (OleDbConnection dbConnection = new OleDbConnection(_connectionSettings))
            {
                dbConnection.Open();
                OleDbCommand query = new OleDbCommand();
                query.Connection = dbConnection;
                query.CommandText=String.Format("DELETE FROM Documents WHERE Id = {0}",id);
                query.ExecuteNonQuery();
                query.Connection.Close();
            }
        }

    }
}
