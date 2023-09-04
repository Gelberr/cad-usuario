using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
using System.Data;
using System.Transactions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CadUsuario.Dados
{
    public class Conexao: IDisposable
    {

        private SqlConnection _connection;
        private SqlTransaction transaction = null;
        public bool transactionIsOpen = false;

        public Conexao()
        {
            try
            {
                _connection = new SqlConnection(StringDeConexao.conString);

            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Dispose()
        {
            this._connection.Dispose();
        }

        public void Open()
        {
            if (_connection != null && _connection.State == ConnectionState.Closed)
            {
                this._connection.Open();
            }
        }

        public void Close()
        {
            if (_connection != null && _connection.State == ConnectionState.Open)
            {
                this._connection.Close();
            }
        }

        public void BeginTransaction()
        {
            if (_connection != null && _connection.State == ConnectionState.Open)
            {
                if (transaction == null && !transactionIsOpen)
                {
                    transaction = _connection.BeginTransaction();
                    transactionIsOpen = true;
                }
            }
        }

        public void CommitTransaction()
        {
            if (_connection != null && _connection.State == ConnectionState.Open)
            {
                if (transaction != null && transactionIsOpen)
                {
                    transaction.Commit();
                    transactionIsOpen = false;
                }
            }

        }

        public bool IsOpen()
        {
            return this._connection.State == ConnectionState.Open;
        }


        public bool ExisteTransacao()
        {
            return transactionIsOpen;
        }

        public void RollBackTransaction()
        {
            if (_connection != null && _connection.State == ConnectionState.Open)
            {
                if (transaction != null && transactionIsOpen)
                {
                    transaction.Rollback();
                    transactionIsOpen = false;
                }
            }
        }

        public SqlDataReader ExecuteReaderParametroLista(string query, List<SqlParameter> parameters)
        {
            try
            {

                using (SqlCommand sqlCommand = new SqlCommand(query, _connection))
                {

                    sqlCommand.Parameters.AddRange(parameters.ToArray());

                    return sqlCommand.ExecuteReader();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public SqlDataReader ExecuteReader(string query)
        {
            try
            {
                using (SqlCommand sqlCommand = new SqlCommand(query, _connection))
                {
                    return sqlCommand.ExecuteReader();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public SqlDataReader ExecuteReader(string query, SqlParameter parameters)
        {
            try
            {

                using (SqlCommand sqlCommand = new SqlCommand(query, _connection))
                {

                    sqlCommand.Parameters.Add(parameters);

                    return sqlCommand.ExecuteReader();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int ExecuteNonQuery(string query, List<SqlParameter> parameters)
        {
            try
            {
                using (SqlCommand sqlCommand = new SqlCommand(query, _connection))
                {
                    sqlCommand.Parameters.AddRange(parameters.ToArray());

                    return sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int ExecuteNonQuery(string query)
        {
            try
            {
                using (SqlCommand sqlCommand = new SqlCommand(query, _connection))
                {
                    return sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public int ExecuteNonQueryTransaction(string query, List<SqlParameter> parameters)
        {
            try
            {
                using (SqlCommand sqlCommand = new SqlCommand(query, _connection, transaction))
                {
                    sqlCommand.Parameters.AddRange(parameters.ToArray());

                    return sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        // --- Início - Stored Procedure ---
        public SqlDataReader ExecuteReader_SP(string procedire)
        {
            try
            {
                using (SqlCommand sqlCommand = new SqlCommand(procedire, _connection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    
                    return sqlCommand.ExecuteReader();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public SqlDataReader ExecuteReader_SP(string procedire, List<SqlParameter> parameters)
        {
            try
            {
                using (SqlCommand sqlCommand = new SqlCommand(procedire, _connection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddRange(parameters.ToArray());

                    return sqlCommand.ExecuteReader();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        // --- Fim - Stored Procedure ---


        //public long ExecuteCommandLastInsertedId(string query, List<SqlParameter> parameters)
        //{
        //    try
        //    {
        //        using (SqlCommand sqlCommand = new SqlCommand(query, _connection))
        //        {
        //            sqlCommand.Parameters.AddRange(parameters.ToArray());

        //            int result = sqlCommand.ExecuteNonQuery();

        //            return sqlCommand.;

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}

        //public long ExecuteCommandLastInsertedId(string query)
        //{
        //    try
        //    {
        //        using (SqlCommand sqlCommand = new SqlCommand(query, _connection))
        //        {
        //            int result = sqlCommand.ExecuteNonQuery();

        //            return sqlCommand.LastInsertedId;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}
    }
}
